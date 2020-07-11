using Ghoul.Presentation.Model.Build;
using Microsoft.AspNetCore.Mvc;

namespace Ghoul.Web.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class BuildController : ControllerBase
    {

        [HttpGet("/")]
        public IActionResult Index()
        {
            // Dispatch a "query" to get all the builds available
            return Ok();
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