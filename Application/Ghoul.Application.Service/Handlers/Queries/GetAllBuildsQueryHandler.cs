using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ghoul.Application.Model;
using Ghoul.Application.Model.Build;
using Ghoul.Application.Model.Queries;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ghoul.Application.Service.Handlers.Queries {

    // Questo è il suo handler

    public class GetAllBuildsQueryHandler : IRequestHandler<GetAllBuildsQuery, BaseBuildApplicationModel[]>
    {
        private readonly ILogger<GetAllBuildsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IReadRepository<BuildPersistenceModel> _buildRepository;

        public GetAllBuildsQueryHandler (
            ILogger<GetAllBuildsQueryHandler> logger,
            IMapper mapper,
            IReadRepository<BuildPersistenceModel> buildRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _buildRepository = buildRepository;
        }

        public Task<BaseBuildApplicationModel[]> Handle(GetAllBuildsQuery request, CancellationToken cancellationToken)
        {
            // Qui automapper lo uso un po' diversamente
            //
            // IN PRIMIS:
            // Non uso il domain layer qui: trasformo direttamente da persistence -> ad application
            // E' un suggerimento del creatore del CQRS, e lo trovi anche nell'articolo di Medium che ti ho linkato
            // Il suggerimento è "Nei command va usato il domain layer, perché rischi di modificare lo stato
            // delle cose in uno stato non consentito", "Nelle query invece non serve, puoi (per velocità e semplicità)"
            // usare direttamente il persistence layer e mapparlo sull'application layer
            //
            // SECONDO:
            // Utilizzo "ProjectTo<BaseBuildApplicationModel>(_mapper.ConfigurationProvider)".
            //
            // Avrei potuto salvare il risultato della FindAll in una variabile
            // e fare un _mapper.Map<BuildApplicationModel[]>(buildPersistenceModels);
            //
            // La differenza è che ProjectTo fa una proiezione sul database
            // Per proiezione si intende una SELECT su una query
            //
            // Per proiezione si intende che se io ho sul database la tabella:
            // USER [ Id, Name, Lastname, Phone ]
            //
            // Nel DbContext ho mappato Id, Name, Lastname e Phone
            // 
            // e nello UserAPPLICATIONModel stanno solo Name e Lastname
            // la project to mi farà "SELECT Name, Lastname FROM User"
            //
            // anziché prendermi TUTTO lo USER e convertirmelo in memoria
            //
            // Praticamente, se UserPersistenceModel è lo User contenente Id,Name,Lastname e Phone
            // Se io faccio una query _USERPERSISTENCEMODELREPOSITORY.FindAll().ProjectTo<UserAPPLICATIONModel>()
            // mi restituirà UserApplicationModel.

            // Avendo lo UserApplicationModel solo Name e Lastname, anche la query sul db sarà relativa
            // solo a Name e Lastname

            var buildApplicationModels = _buildRepository
                .FindAll()
                // Qui faccio una ProjectTo su BASEBuildApplicationModel.. E' un base, quindi è un modello più piccolo
                .ProjectTo<BaseBuildApplicationModel>(_mapper.ConfigurationProvider);


            // Tornando qui nell'handler, essendo stata richiesta una lista di build con pochi dati,
            // con la proiezione su BASEBuildApplicationModel ottengo solo i dati che mi servono



            // 'Sta cosa del mapping delle proprietà che ti servono solamente, funziona perché la repository
            // restituisce un oggetto di tipo IQueryable, e non un IEnumerable

            // Praticamente, finché la query è in stato di "IQueryable", non è stata ancora materializzata
            // cioè non è stata creata nessuna stringa sql per fare la richiesta sul DB
            //
            // Quando l'IQueryable viene convertito in un IEnumerable, con un ToList() o un First(), ad esempio
            // viene generata la query sql e lanciata sul DB.
            //
            // E' un meccanismo che sta alla base del funzionamento di entity framework, e va conosciuta bene
            // perché altrimenti si potrebbero incorrere in grossi problemi di performance


            /*
            *
            *   _buildRepository.FindAll()  // IQueryable
            *       .ToList()               // IEnumerable
            *       .Where(x => x.Enabled)  // IEnumerable
            *       .Select(x => x.Name)    // Conversione (proiezione) in memoria
            *
            *   SELECT * FROM tabella
            *
            *   _buildRepository.FindAll()  // IQueryable
            *       .Where(x => x.Enabled)  // IQueryable
            *       .Select(x => x.Name)    // string[]
            *       .ToList()               // IList<string> ( o IEnumerable<string> )
            *
            *   SELECT Name From tabella where Enabled = 1
            *
            **/
            _logger.LogTrace(buildApplicationModels.ToString());

            return Task.FromResult(buildApplicationModels.ToArray());
        }
    }
}