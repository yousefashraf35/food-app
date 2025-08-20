public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<MenuItem>? MenuItems { get; set; } = new List<MenuItem>();
}