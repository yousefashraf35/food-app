namespace FoodApp.DTOs.Restaurants;

public class RestaurantDto
{ 
    public int Id { get; set; }
    public required string Name { get; set; }
    public CuisineType CuisineType { get; set; } = CuisineType.Other;
    public double? DeliveryRadius { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Location { get; set; }
}