using System.Diagnostics;
using System.Threading.Tasks;
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
        public async Task<string[]> Index()
        {
            var id = await _mediator.Send(new CreateBuildCommand("sed"));
            return await _mediator.Send(new GetAllBuildsQuery());
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