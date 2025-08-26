using System.ComponentModel.DataAnnotations;

namespace FoodApp.DTOs.Orders
{

    public class OrderItemDto
    {
        [Required]
        public int MenuItemId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
        
        [Required]
        public double Price { get; set; }
    }
}