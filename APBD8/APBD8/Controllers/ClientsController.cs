using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private IService _iService;
        public ClientsController(IService iService)
        {
            _iService = iService;
        }

        [HttpGet("{id}/trips")]
        public async Task<IActionResult> GetClientTrips(string id)
        {
            var clientTrips = await _iService.GetClientTripsAsync(id);
            if (clientTrips.IdClient == 0 ) 
            {
                return NotFound();
            }
            return Ok(clientTrips);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostClients(ClientDTO newClient )
        {
         var client =   await _iService.PostNewClientAsync(newClient.FirstName, newClient.LastName, newClient.Email, newClient.Telephone,newClient.Pesel);
            return Ok(client);
        }

        [HttpPut("{id}/trips/{tripId}")]
        public async Task<IActionResult> PutClientTrip(string id, string tripId)
        {
          
            var status = await _iService.PutNewRegisteredClientTrip(id, tripId);
            
            if (status == 404)
            {
               return NotFound();
            }
            if (status == 409)
            {
                return BadRequest("no places available");
            }
            
            return Ok();
        }

        [HttpDelete("{id}/trips/{tripId}")]
        public async Task<IActionResult> DeleteClientTrip(string clientId, string tripId)
        {
            await _iService.DeleteRegisteredClientTrip(clientId, tripId);
            return Ok();
        }
        
    }
}