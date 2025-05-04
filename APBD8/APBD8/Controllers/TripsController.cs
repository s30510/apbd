using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IService _tripsService;
        
        public TripsController(IService tripsService)
        {
            _tripsService = tripsService;
        }
        
        [HttpGet]
        public async Task<IActionResult>GetTrips()
        {
            var trips = await _tripsService.GetTripsAsync();
           
            return Ok(trips);
        }
        
    }
    
}
