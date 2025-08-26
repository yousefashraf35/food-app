using System.ComponentModel.DataAnnotations;
using FoodApp.DTOs.Restaurants;

namespace FoodApp.DTOs.Orders
{
    public class CreateOrderDto
    {
        [Required]
        public required string ApplicationUserId { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public AddressDto? Address { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();

    }
}