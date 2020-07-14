using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Ghoul.Application.Model;
using Ghoul.Application.Model.Build;
using Ghoul.Application.Model.Commands;
using Ghoul.Application.Model.Queries;
using Ghoul.Presentation.Model;
using Ghoul.Presentation.Model.Build;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ghoul.Presentation.Web.Controllers.API.Build {

    [ApiController]
    [Route("api/[controller]")]
    public class BuildController : BaseAPIController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator; // Mediator pattern/abstraction

        public BuildController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<BaseBuildApplicationModel[]> Index()
        {
            return await _mediator.Send(new GetAllBuildsQuery());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) {
            var build = await _mediator.Send(new GetBuildQuery(id));
            if (build == null)
                return NotFound();
            return new JsonResult(build);
        }


        // Richiesta di CREARE una build.
        // In questo caso, essendo una richiesta di SCRITTURA, la richiesta è un COMMAND.
        // Fosse stata una richiesta di LETTURA, sarebbe stata una QUERY.
        // ( Command vs Query - Pattern arhitetturale CQRS )
        [HttpPost]
        // CreateBuildInputModel è un modello PRESENTAZIONALE (sta nella cartella "Presentation/Ghoul.Presentation.Model")
        // Contiene data-attribute di aspnet core per la validazione (e.g. [Require])
        public async Task<IActionResult> CreateBuild(CreateBuildInputModel buildInputModel)
        {
            // TryValidateModel cerca di validare il CreateBuildInputModel lato PRESENTAZIONALE
            if (TryValidateModel(buildInputModel)) {
                try {
                    // Wrappo la richiesta in un try catch, perché più "infondo" nella gerarchia
                    // dell'architettura, potrei beccare qualche eccezione.
                    // Ad esempio, se una logica di DOMINIO o APPLICATIVA non vanno a buon fine,
                    // l'eccezione voglio che arrivi fino alla UI

                    // MEDIATOR
                    // Mediator è un pattern, un'astrazione e in questo caso una libreria.
                    // E' impostata tramite dependency injection nel costruttore
                    var id = await _mediator.Send(_mapper.Map<CreateBuildCommand>(buildInputModel));
                    return Ok(id);
                } catch (Exception exception) {
                    // L'errore di DOMINIO o APPLICAZIONE arriva alla UI tramite questo BadRequest
                    return BadRequest(BadRequestOutputModel.FromException(exception));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBuildInfo(string id, [FromBody]UpdateBuildInfoInputModel updateBuildInfoInputModel) {

            if (TryValidateModel(updateBuildInfoInputModel)) {

            }
            return BadRequest(ModelState);
        }
    }
}