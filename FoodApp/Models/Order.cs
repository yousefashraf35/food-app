using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public Payment? Payment { get; set; }

    [Key]
    public int OrderId { get; set; } // PK
    public DateTime CreatedAt { get; set; } // created_at
    public double TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";

    // FK: ApplicationUser
    public required string ApplicationUserId { get; set; }
    public required ApplicationUser ApplicationUser { get; set; }
    // FK: Address
    public required int AddressId { get; set; }
    public required Address DeliveryAddress { get; set; }
    // FK: Restaurant
    public required int RestaurantId { get; set; }
    public required Restaurant Restaurant { get; set; }
}