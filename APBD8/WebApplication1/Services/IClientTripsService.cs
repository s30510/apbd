using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public interface IClientTripsService
{
    Task< ClientTripsDTO> GetClientTripsAsync(string clientId);
}