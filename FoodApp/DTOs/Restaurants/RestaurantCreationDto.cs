using System.ComponentModel.DataAnnotations;

namespace FoodApp.DTOs.Restaurants;

public class RestaurantCreationDto
{ 
    [Required]
    public required string Name { get; set; }
    public CuisineType CuisineType { get; set; } = CuisineType.Other;
    public double? DeliveryRadius { get; set; }
    [Required]
    public required string PhoneNumber { get; set; }
    [Required]
    public required string Location { get; set; }

}