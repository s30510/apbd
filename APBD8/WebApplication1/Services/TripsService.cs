using Microsoft.Data.SqlClient;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public class TripsService : ITripsService
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True; Trust Server Certificate=True";
    public async Task<List<TripDTO>> GetTripsAsync()
    {
        var trips = new List<TripDTO>();
        var cmdText = "SELECT IdTrip, Name FROM Trip";
        await using (SqlConnection conn = new SqlConnection(_connectionString)) 
        await using (SqlCommand cmd = new SqlCommand(cmdText))
        {
            await conn.OpenAsync();
            
           var reader = await cmd.ExecuteReaderAsync();
           
           int idTripOrdinal = reader.GetOrdinal("IdTrip");

           while (await reader.ReadAsync())
           {
               trips.Add(new TripDTO()
               {
                   IdTrip = reader.GetInt32(idTripOrdinal),
                   Name = reader.GetString(1)
               });
           }
            
        }
        
        return trips;
        
    }
    
}