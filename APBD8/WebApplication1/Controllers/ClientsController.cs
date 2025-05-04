using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private IClientTripsService _clientTripsService;
        
        private INewClientService _newClientService;
        public ClientsController(IClientTripsService clientTripsService, INewClientService newClientService)
        {
            _clientTripsService = clientTripsService;
            _newClientService = newClientService;
        }

        [HttpGet("{id}/trips")]
        public async Task<IActionResult> GetClientTrips(string id)
        {
            var clientTrips = await _clientTripsService.GetClientTripsAsync(id);
            if (clientTrips.IdClient == 0)
            {
                return NotFound();
            }
            
            return Ok(clientTrips);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostClients([FromBody]NewClientDTO newClient )
        {
            await _newClientService.PostNewClientAsync(newClient.FirstName, newClient.LastName, newClient.Email, newClient.Telephone,newClient.Pesel);
            return Created();
        }

        [HttpPut("{id}/trips/{tripId}")]
        public async Task<IActionResult> PutClientTrip(string clientId, string tripId)
        {
            return Ok("PutClient");
        }

        [HttpDelete("{id}/trips/{tripId}")]
        public async Task<IActionResult> DeleteClientTrip(string clientId, string tripId)
        {
            return Ok("DeleteClient");
        }
        
    }
}