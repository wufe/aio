using Ghoul.Presentation.Model.Build;
using Microsoft.AspNetCore.Mvc;

namespace Ghoul.Web.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class BuildController : ControllerBase
    {

        [HttpGet]
        public IActionResult Index() {
            return Ok("Oke");
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