using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ghoul.Application.Model.Commands;
using Ghoul.Domain.Service.Interface;
using Ghoul.Persistence.Model;
using Ghoul.Persistence.Repository.Interface;
using MediatR;

namespace Ghoul.Application.Service.Handlers.Commands {
    // Questo è l'HANDLER (event listener)
    //
    // Implementa l'interfaccia MediatR.IRequestHandler<COMANDO, TIPO RITORNO>
    public class CreateBuildCommandHandler : IRequestHandler<CreateBuildCommand, string> // <--- MediatR.IRequestHandler
    {
        private readonly IMapper _mapper;
        private readonly IBuildDomainService _buildService;
        private readonly IRepository<BuildPersistenceModel> _buildRepository;

        // Essendo configurato nello Startup.cs, 
        // ha accesso al container di dependency injection.
        // Permette dunque di iniettare i repository che ti servono, o l'istanza di automapper
        public CreateBuildCommandHandler(
            IMapper mapper,
            IBuildDomainService buildService,
            IRepository<BuildPersistenceModel> buildRepository)
        {
            _mapper = mapper;
            _buildService = buildService;
            _buildRepository = buildRepository;
        }

        // L'interfaccia IRequestHandler richiede di implementare il metodo Handle
        //
        // Prende in input come primo parametro il CreateBuildCommand (l'evento originale), dispatchato nel controller
        // 
        // Restituisce in output un Task del tipo di ritorno (Definito in IRequestHandler<CreateBuildCommand, TIPO RITORNO>
        // e nel comando IRequest<TIPO RITORNO>).
        public Task<string> Handle(CreateBuildCommand request, CancellationToken cancellationToken)
        {
            // Validate
            var buildAlreadyExists = _buildRepository.FindAll(x => x.Name.ToLower() == request.Name.ToLower()).Any();
            if (buildAlreadyExists)
                throw new ArgumentException($"Build named \"{request.Name}\" already exists.");

            // Create domain entity
            var buildDomainEntity = _buildService.CreateBuild(request.Name);
            if (request.RepositoryURL != null)
                buildDomainEntity.SetRepository(request.RepositoryURL);

            // Convert domain entity to persistence model
            var buildPersistenceModel = _mapper.Map<BuildPersistenceModel>(buildDomainEntity);

            // Store persistence model
            _buildRepository.Insert(buildPersistenceModel);
            return Task.FromResult(buildPersistenceModel.ID);
        }
    }
}