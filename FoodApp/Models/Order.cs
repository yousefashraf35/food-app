using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [Key]
    public int OrderId { get; set; } // PK



    public DateTime CreatedAt { get; set; } // created_at

    public decimal TotalAmount { get; set; }

    public string Status { get; set; }

    public Payment Payment { get; set; }

    // FK: ApplicationUser
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    // FK: Address
    public int AddressId { get; set; }
    public Address DeliveryAddress { get; set; }

    // FK: Restaurant
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
}