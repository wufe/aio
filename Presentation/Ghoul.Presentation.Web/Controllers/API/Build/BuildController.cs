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
            // Prendi in considerazione questa GetAllBuildsQuery
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

                    // Istruisco la libreria AUTOMAPPER - che si occupa di copiare i valori da un modello ad un altro -
                    // di convertirmi un CreateBuildInputModel ( modello presentazionale ) in COMMAND.
                    //
                    // La configurazione di questo mapping sta inizializzata nello startup.
                    var createBuildCommand = _mapper.Map<CreateBuildCommand>(buildInputModel);

                    // FINITO IL MAPPING, abbiamo un oggetto di tipo CreateBuildCommand { Name = "Ciccio" }
                    //
                    // Come facciamo a farlo finire a livello applicativo,
                    // nei suoi service, per fargli scrivere 'sta cosa nel DB?
                    //
                    // Usiamo un mediator pattern.
                    //
                    // La libreria usata è MediatR.
                    // E' stata importata tramite dependency injection nel costruttore di questo controller
                    // e viene configurata nello startup.
                    //
                    // Il pattern mediator vedilo come un "bus" di eventi
                    // Qui dico "buttami nel bus, il comando "CreateBuildCommand""
                    //
                    // Per prendere in considerazione lo stesso pattern con un esempio banale, applicato al javascript
                    // il MEDIATOR può essere il "document"
                    //
                    // Lì, anziché fare document.Send, si fa document.dispatchEvent(new ClickEvent) ( mi pare)
                    // https://developer.mozilla.org/en-US/docs/Web/API/EventTarget/dispatchEvent sì
                    //
                    // Ad esempio, quando fai click su un DIV, il browser fa DIV.dispatchEvent(new ClickEvent)
                    // In questa analogia con il javascript nel browser, il DOCUMENT è il MEDIATOR.
                    // E' cioè un oggetto che contiene un metodo "dispatch" e accetta come argomento un evento.
                    //
                    // Per continuare l'analogia con javascript, quando si vuole intercettare un click,
                    // la sintassi è DOCUMENT.addEventListener('click', FUNZIONE)
                    //
                    // Questo permette di intercettare l'evento dispatchato dal browser (DIV.dispatchEvent(new ClickEvent))
                    //
                    // Se ti metti in ascolto 100 volte dello stesso evento, scrivendo 100 volte DOCUMENT.addEventListener('click')
                    // la funzione di callback dell'addEventListener verrà richiamata 100 volte
                    //
                    // Quindi, continuando su questa analogia, il DOCUMENT, in quanto MEDIATOR,
                    // oltre alla funzione "DISPATCH", ha anche un "ADDEVENTLISTENER"

                    // In questa applicazione il mediator accetta tramite il comando "Send"
                    // il dispatch di un evento
                    // Ci sarà quindi da qualche altra parte un componente che farà tipo
                    // "mediator.addEventListener('CreateBuildCommand', function(){ crea la build })" <-- PSEUDO CODICE

                    // Mediator, con i suoi COMMAND/QUERY (eventi) e i suoi HANDLER (event listeners) è configurato nello Startup.

                    // Supponendo di non voler avere il mediator pattern con gli handler
                    // si ha un application service (application layer)
                    //
                    // _buildApplicationService.CreateNewBuild(createBuild)
                    //
                    // Da notare che ho usato "buildInputModel", il modello PRESENTAZIONALE con le validazioni di aspnetcore
                    // Quindi in realtà potrebbe essere un po' complicato
                    // ( a livello di referenze tra progetti) 

                    var id = await _mediator.Send(buildInputModel);

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