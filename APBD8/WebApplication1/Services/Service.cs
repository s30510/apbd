using Microsoft.Data.SqlClient;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public class Service : ITripsService, IClientTripsService, INewClientService
{
    private readonly string _connectionString =
        "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";

    public async Task<List<TripDTO>> GetTripsAsync()
    {
        var trips = new Dictionary<int, TripDTO>();

        var cmdText =
            "SELECT Trip.IdTrip, Trip.Name, Trip.Description, Trip.DateFrom, Trip.DateTo, Trip.MaxPeople, Country.Name AS CountryName " +
            "FROM Trip " +
            "JOIN Country_Trip ON Trip.IdTrip = Country_Trip.IdTrip " +
            "JOIN Country ON Country_Trip.IdCountry = Country.IdCountry " +
            "ORDER BY Trip.IdTrip";


        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(cmdText, conn))
        {
            await conn.OpenAsync();

            var reader = await cmd.ExecuteReaderAsync();

            int idTripOrdinal = reader.GetOrdinal("IdTrip");

            while (await reader.ReadAsync())
            {
                int idTrip = reader.GetInt32(idTripOrdinal);


                if (!trips.ContainsKey(idTrip))
                {
                    trips[idTrip] = new TripDTO
                    {
                        IdTrip = idTrip,
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        DateFrom = reader.GetDateTime(3),
                        DateTo = reader.GetDateTime(4),
                        Maxpeople = reader.GetInt32(5),
                        Countries = new List<object>()
                    };
                }

                trips[idTrip].Countries.Add(reader.GetString(6));
            }
        }

        return trips.Values.ToList();
    }

    public async Task<ClientTripsDTO> GetClientTripsAsync(string clientId)
    {
        var clientTrips = new ClientTripsDTO();

        var query = "SELECT IdClient, FirstName, LastName FROM Client WHERE IdClient =@clientId";

        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@clientId", clientId);
            await conn.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            int idClientOrdinal = reader.GetOrdinal("IdClient");


            if (await reader.ReadAsync())
            {
                clientTrips = new ClientTripsDTO
                {
                    IdClient = reader.GetInt32(idClientOrdinal),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2)
                };
            }
        }


        return clientTrips;
    }

    public async Task<int> PostNewClientAsync(string firstName, string lastName, string email, string telephone,
        string pesel)
    {
        string txtComand = "INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel)\nVALUES (@FirstName, @LastName, @Email, @Telephone, @Pesel) ";


        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(txtComand, conn))
        {
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Telephone", telephone);
            cmd.Parameters.AddWithValue("@Pesel", pesel);
            
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync();
            
        }
    }
}