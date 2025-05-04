using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public interface INewClientService 
{
    Task<int> PostNewClientAsync(string firstName, string lastName,string email,  string telephone, string pesel);
    
}