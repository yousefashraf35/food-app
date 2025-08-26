using System.ComponentModel.DataAnnotations;

namespace FoodApp.DTOs.Restaurants;

public class AddressDto
{
    public int? Floor { get; set; }
    [Required]
    public required string Building { get; set; }
    [Required]
    public required string Street { get; set; }
    [Required]
    public required string City { get; set; }
}
