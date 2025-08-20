public class Address
{
 public ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public int Id { get; set; }
    public int? Floor { get; set; }
    public string Building { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public required string ApplicationUserId { get; set; }
    public required ApplicationUser ApplicationUser { get; set; }


}
