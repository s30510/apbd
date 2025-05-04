using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs;


public class TripDTO
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; } 
    public int MaxPeople { get; set; }
    public List<object> Countries { get; set; }
    public int RegisteredAt { get; set; }
    public int? PaymentDate { get; set; } 
}

public class ClientTripsDTO
{
   public int IdClient { get; set; }
   public string FirstName { get; set; }
   public string LastName { get; set; }
   public List<TripDTO> Trips { get; set; }
}

public class ClientDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [RegularExpression("\\+48\\d{9}")]
    public string Telephone { get; set; }
    [RegularExpression("\\d{11}")]
    public string Pesel { get; set; }
}








