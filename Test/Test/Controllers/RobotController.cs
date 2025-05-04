using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Services;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobotController(IService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetRobots()
        {
            var robots = await service.GetRobots();
            return Ok(robots);
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> PostRobot(string name)
        {
          await  service.PostRobot(name);
            return Created();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await service.DeleteRobot(name);
            return Ok();
        }
    }
}
