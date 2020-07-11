using System.Diagnostics;
using System.Threading.Tasks;
using Ghoul.Application.Model;
using Ghoul.Application.Model.Commands;
using Ghoul.Application.Model.Queries;
using Ghoul.Presentation.Model.Build;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ghoul.Web.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class BuildController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BuildController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<BuildApplicationModel[]> Index()
        {
            return await _mediator.Send(new GetAllBuildsQuery());
        }

        [HttpGet("add/{name}")]
        public async Task<string> Add(string name)
        {
            return await _mediator.Send(new CreateBuildCommand(name));
        }

        [HttpPost]
        public IActionResult CreateBuild(CreateBuildInputModel buildModel)
        {
            if (TryValidateModel(buildModel)) {

                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}