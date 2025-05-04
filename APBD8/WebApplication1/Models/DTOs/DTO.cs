namespace WebApplication1.Models.DTOs;


public class TripDTO
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; } 
    public int Maxpeople { get; set; }
    public List<object> Countries { get; set; }
}

public class ClientTripsDTO
{
   public int IdClient { get; set; }
   public string FirstName { get; set; }
   public string LastName { get; set; }
   public List<TripDTO> Trips { get; set; }
}

public class NewClientDTO
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Pesel { get; set; }
}






