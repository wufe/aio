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
        private readonly IMediator _mediator;

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

        [HttpPost]
        public async Task<IActionResult> CreateBuild(CreateBuildInputModel buildInputModel)
        {
            if (TryValidateModel(buildInputModel)) {
                try {
                    var id = await _mediator.Send(_mapper.Map<CreateBuildCommand>(buildInputModel));
                    return Ok(id);
                } catch (Exception exception) {
                    return BadRequest(exception.Message);
                    // return BadRequest(BadRequestOutputModel.FromException(exception));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBuild(string id, [FromBody]UpdateBuildInputModel inputModel) {

            if (TryValidateModel(inputModel)) {
                try {
                    await _mediator.Send(_mapper.Map<UpdateBuildInputModel, UpdateBuildCommand>(inputModel, UpdateBuildCommand.FromBuildID(id)));
                    return Ok();
                } catch (Exception exception) {
                    return BadRequest(BadRequestOutputModel.FromException(exception));
                }
                
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuild(string id) {
            try {
                await _mediator.Send(new DeleteBuildCommand(id));
                return Ok();
            } catch (Exception exception) {
                return BadRequest(BadRequestOutputModel.FromException(exception));
            }
        }

        [HttpPost("{buildID}/step")]
        public async Task<IActionResult> CreateStep(string buildID, [FromBody] CreateStepInputModel inputModel) {
            if (TryValidateModel(inputModel)) {
                try {
                    var command = _mapper.Map<CreateStepInputModel, CreateStepCommand>(inputModel, CreateStepCommand.FromBuild(buildID));
                    var id = await _mediator.Send(command);
                    return Ok(id);
                } catch (Exception exception) {
                    return BadRequest(BadRequestOutputModel.FromException(exception));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("{buildID}/step/{stepIndex}")]
        public async Task<IActionResult> UpdateStep(string buildID, int stepIndex, [FromBody] UpdateStepInputModel inputModel) {
            if (TryValidateModel(inputModel)) {
                try {
                    var command = _mapper.Map<UpdateStepInputModel, UpdateStepCommand>(inputModel, UpdateStepCommand.FromBuild(buildID, stepIndex));
                    await _mediator.Send(command);
                    return Ok();
                } catch (Exception exception) {
                    return BadRequest(BadRequestOutputModel.FromException(exception));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{buildID}/step/{stepIndex}")]
        public async Task<IActionResult> DeleteStep(string buildID, int stepIndex)
        {
            try
            {
                await _mediator.Send(new DeleteStepCommand(buildID, stepIndex));
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(BadRequestOutputModel.FromException(exception));
            }
        }

        [HttpGet("{buildID}/run/latest")]
        public async Task<RunApplicationModel> GetLatestRun(string buildID) {
            var run = await _mediator.Send(GetLatestRunQuery.FromBuild(buildID));
            return run;
        }

        [HttpPost("{buildID}/run")]
        public async Task<IActionResult> EnqueueRun(string buildID) {
            await _mediator.Send(new EnqueueRunCommand(buildID));
            return Ok();
        }
    }
}