public class Restaurant
{
    public int Id { get; set; }
    public required string Name { get; set; }
     public CuisineType CuisineType { get; set; } = CuisineType.Other;
    public double? DeliveryRadius { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Location { get; set; }

    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}