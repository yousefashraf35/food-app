using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public required int MenuItemId { get; set; }
    public required MenuItem MenuItem { get; set; }
    public required int OrderId { get; set; }
    public required Order Order { get; set; }
    
}
