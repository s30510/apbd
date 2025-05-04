using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public interface IService
{
    Task< List<TripDTO>> GetTripsAsync();
    Task< ClientTripsDTO> GetClientTripsAsync(string clientId);
    Task<ClientDTO> PostNewClientAsync(string firstName, string lastName,string email,  string telephone, string pesel);
    Task<int> PutNewRegisteredClientTrip(string clientId, string tripId);
    Task<int> DeleteRegisteredClientTrip(string clientId, string tripId);
}