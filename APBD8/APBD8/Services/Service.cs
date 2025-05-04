using Microsoft.Data.SqlClient;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public class Service : IService
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
                        MaxPeople = reader.GetInt32(5),
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


        var query =
            "SELECT Client.IdClient,FirstName,LastName, Trip.IdTrip, Trip.Name, Trip.Description, Trip.DateFrom, Trip.DateTo, Trip.MaxPeople, RegisteredAt, PaymentDate, Country.Name FROM Client\nINNER JOIN Client_Trip ON Client_Trip.IdClient=Client.IdClient\nINNER JOIN Trip ON Client_Trip.IdTrip=Trip.IdTrip INNER JOIN Country_Trip ON Trip.IdTrip = Country_Trip.IdTrip\nINNER JOIN Country ON Country_Trip.IdCountry = Country.IdCountry\nWHERE Client.IdClient=@ClientId";

        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@clientId", clientId);
            await conn.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            int idClientOrdinal = reader.GetOrdinal("IdClient");


            if (await reader.ReadAsync())
            {
                var trips = new List<TripDTO>();

                clientTrips = new ClientTripsDTO
                {
                    IdClient = reader.GetInt32(idClientOrdinal),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Trips = trips
                };

                int idTrip = reader.GetInt32(3);

                trips.Add(new TripDTO()
                {
                    IdTrip = idTrip,
                    Name = reader.GetString(4),
                    Description = reader.GetString(5),
                    DateFrom = reader.GetDateTime(6),
                    DateTo = reader.GetDateTime(7),
                    MaxPeople = reader.GetInt32(8),
                    Countries = new List<object> { reader.GetString(11) },
                    RegisteredAt = reader.GetInt32(9),
                    PaymentDate = reader.IsDBNull(10) ? null : reader.GetInt32(10)
                });


                while (await reader.ReadAsync())
                {
                    int newIdTrip = reader.GetInt32(3);

                    if (newIdTrip != idTrip)
                    {
                        trips.Add(new TripDTO()
                        {
                            IdTrip = newIdTrip,
                            Name = reader.GetString(4),
                            Description = reader.GetString(5),
                            DateFrom = reader.GetDateTime(6),
                            DateTo = reader.GetDateTime(7),
                            MaxPeople = reader.GetInt32(8),
                            Countries = new List<object> { reader.GetString(11) },
                            RegisteredAt = reader.GetInt32(9),
                            PaymentDate = reader.IsDBNull(10) ? null : reader.GetInt32(10)
                        });
                    }
                    else
                    {
                        foreach (var i in trips)
                        {
                            if (i.IdTrip == idTrip)
                            {
                                i.Countries.Add(reader.GetString(11));
                            }
                        }
                    }
                }
            }
        }


        return clientTrips;
    }

    public async Task<ClientDTO> PostNewClientAsync(string firstName, string lastName, string email, string telephone,
        string pesel)
    {
        string insertClientCommand =
            "INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel)\nVALUES (@FirstName, @LastName, @Email, @Telephone, @Pesel) ";

        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(insertClientCommand, conn))
        {
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Telephone", telephone);
            cmd.Parameters.AddWithValue("@Pesel", pesel);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            return new ClientDTO
                { FirstName = firstName, LastName = lastName, Email = email, Telephone = telephone, Pesel = pesel };
        }
    }

    public async Task<int> PutNewRegisteredClientTrip(string clientId, string tripId)
    {
        int maxPeople;

        await using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                //sprawdzamy czy klient o podanym id istnieje
                await using (SqlCommand sqlCommand =
                             new SqlCommand("SELECT 1 WHERE EXISTS(SELECT 1 FROM Client WHERE IdClient = @clientId)",
                                 connection, transaction))
                {
                    sqlCommand.Parameters.AddWithValue("@clientId", clientId);
                    var reader = await sqlCommand.ExecuteReaderAsync();
                    if (!await reader.ReadAsync())
                    {
                        return 404;
                    }

                    reader.Close();
                }

                //sprawdzamy czy wycieczka o podanym id istnieje a przy okazji pobieramy informacje o maksymalnej liczbie osób dla danej wycieczki
                await using (SqlCommand sqlCommand =
                             new SqlCommand("SELECT MaxPeople FROM Trip WHERE IdTrip = @tripId",
                                 connection, transaction))
                {
                    sqlCommand.Parameters.AddWithValue("@tripId", tripId);
                    var reader = await sqlCommand.ExecuteReaderAsync();
                    if (!await reader.ReadAsync())
                    {
                        return 404;
                    }

                    maxPeople = reader.GetInt32(0);
                    reader.Close();
                }

                //pobieramy informacje o rzeczywistej liczbie osób zarejestrownych na wycieczke, żeby sprawdzić czy jest jeszcze miejsce dla nowo dodawanej osoby
                await using (SqlCommand sqlCommand =
                             new SqlCommand(
                                 " SELECT COUNT(Client.IdClient) FROM Client\n INNER JOIN Client_Trip ON Client_Trip.IdClient=Client.IdClient\n INNER JOIN Trip ON Client_Trip.IdTrip = Trip.IdTrip",
                                 connection, transaction))
                {
                    var reader = await sqlCommand.ExecuteReaderAsync();

                    int peopleCount = 0;
                    if (await reader.ReadAsync())
                    {
                        peopleCount = reader.GetInt32(0);
                    }

                    if (peopleCount > maxPeople)
                    {
                        return 409;
                    }

                    reader.Close();
                }

                //wstawiamy nową rejestracje do bazy
                await using (SqlCommand sqlCommand =
                             new SqlCommand(
                                 "INSERT INTO Client_Trip (IdClient,IdTrip,RegisteredAt)VALUES (@IdClient, @IdTrip, @RegisteredAt)",
                                 connection, transaction))
                {
                    Console.Write("1");
                    sqlCommand.Parameters.AddWithValue("@IdClient", clientId);
                    sqlCommand.Parameters.AddWithValue("@IdTrip", tripId);
                    sqlCommand.Parameters.AddWithValue("@RegisteredAt",
                        (int)((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds());
                    await sqlCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

            transaction.Commit();
        }

        return 200;
    }

    public async Task<int> DeleteRegisteredClientTrip(string clientId, string tripId)
    {
        
        await using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            SqlTransaction transaction = conn.BeginTransaction();

            
            //sprawdzamy czy rejestracja istnieje
            await using (SqlCommand cmd = new SqlCommand(
                             "SELECT * FROM Client\n INNER JOIN Client_Trip ON Client_Trip.IdClient=Client.IdClient\n INNER JOIN Trip ON Client_Trip.IdTrip = Trip.IdTrip\n WHERE Client_Trip.IdClient=@IdClient AND Client_Trip.IdTrip = @IdTrip",
                             conn, transaction))
            {
                
                cmd.Parameters.AddWithValue("@IdClient", clientId);
                cmd.Parameters.AddWithValue("@IdTrip", tripId);

                var reader = await cmd.ExecuteReaderAsync();

                if (!await reader.ReadAsync())
                {
                    return 404;
                }
                reader.Close();
            }
       
            //usuwamy rejestracje
            await using (SqlCommand cmd = new SqlCommand("DELETE FROM Client_Trip WHERE IdClient = @IdClient AND IdTrip = @IdTrip", conn,
                             transaction))
            {
                cmd.Parameters.AddWithValue("@IdClient", clientId);
                cmd.Parameters.AddWithValue("@IdTrip", tripId);
                
                await cmd.ExecuteNonQueryAsync();
                transaction.Commit();
                return 200;
            }
            
        }
    }
}