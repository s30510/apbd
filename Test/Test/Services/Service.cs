using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;
using Test.Models.DTOs;

namespace Test.Services;

public class Service : IService
{
    private readonly string connectionString =
        "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";


    public async Task<List<RobotDto>> GetRobots()
    {
        var robots =new  List<RobotDto>() ;
        string txtComannd = "SELECT * FROM Robot";

        await using (SqlConnection connection = new SqlConnection(connectionString))
        await using (SqlCommand command = new SqlCommand(txtComannd, connection))
        {
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                robots.Add(new RobotDto()
                {
                    id = reader.GetInt32(0),
                    name = reader.GetString(1)
                });
            }
        }

        return robots;
    }

    public async Task<bool> PostRobot(string name)
    {
        var txtcmd = "INSERT INTO ROBOT (imie) VALUES (@imie)";
        
        await using (SqlConnection connection = new SqlConnection(connectionString))
        await using (SqlCommand command = new SqlCommand(txtcmd, connection))
        {
            await connection.OpenAsync();
     
            command.Parameters.AddWithValue("@imie", name);
           int rowAffected =  await command.ExecuteNonQueryAsync();
            return rowAffected >0;
        }
        
    }

    public async Task<bool> DeleteRobot(string name)
    {
       var txtcmd = "DELETE FROM Robot WHERE id=@id;";
       await using (SqlConnection connection = new SqlConnection(connectionString))
       await using (SqlCommand command = new SqlCommand(txtcmd, connection))
       {
           await connection.OpenAsync();
           command.Parameters.AddWithValue("@id", name);
           int rowAffected = await command.ExecuteNonQueryAsync();
           return rowAffected >0;
       }

    }
}