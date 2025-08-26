namespace FoodApp.DTOs.MenuItems;

public class MenuItemCreationDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string? CategoryName { get; set; }
    
}