public class MenuItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public bool IsAvailable { get; set; } = true;

    public required int CategoryId { get; set; }
    public Category? Category { get; set; }
    public required int RestaurantId { get; set; }
    public required Restaurant Restaurant { get; set; }
}