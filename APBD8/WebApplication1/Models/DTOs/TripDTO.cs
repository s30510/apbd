namespace WebApplication1.Models.DTOs;

public class TripDTO
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public List<object> Countries { get; set; }
}




public class CountriesDTO
{
    public string Name { get; set; }
}