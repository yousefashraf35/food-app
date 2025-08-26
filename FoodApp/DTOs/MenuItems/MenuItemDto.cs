namespace FoodApp.DTOs.MenuItems;

public class MenuItemDto
{   
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; } = true;
    public int? CategoryId { get; set; }
    public int RestaurantId { get; set; }
    
}