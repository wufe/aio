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

namespace Ghoul.Presentation.Web.Controllers.API {

    [ApiController]
    [Route("api/[controller]")]
    public class BuildController : BaseAPIController
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
        public async Task<BuildBaseApplicationModel[]> Index()
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

        [HttpPost]
        public async Task<IActionResult> CreateBuild(CreateBuildInputModel buildInputModel)
        {
            if (TryValidateModel(buildInputModel)) {
                try {
                    var id = await _mediator.Send(_mapper.Map<CreateBuildCommand>(buildInputModel));
                    return Ok(id);
                } catch (Exception exception) {
                    return BadRequest(BadRequestOutputModel.FromException(exception));
                }
            }
            return BadRequest(ModelState);
        }
    }
}