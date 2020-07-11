using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public BuildController(
            IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<BuildApplicationModel[]> Index()
        {
            return await _mediator.Send(new GetAllBuildsQuery());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBuild(CreateBuildInputModel buildInputModel)
        {
            if (TryValidateModel(buildInputModel)) {

                var id = await _mediator.Send(_mapper.Map<CreateBuildCommand>(buildInputModel));
                return Ok(id);
            }
            return BadRequest(ModelState);
        }
    }
}