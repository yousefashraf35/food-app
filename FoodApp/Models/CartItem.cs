using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    [Key]
    public int CartItemId { get; set; } // PK


    public int MenuItemId { get; set; } // FK to MenuItem
    public MenuItem MenuItem { get; set; } // Navigation property


    public int Quantity { get; set; } // Quantity of the item in the cart


    public string ApplicationUserId { get; set; } // FK to ApplicationUser
    public ApplicationUser ApplicationUser { get; set; } // Navigation property
}