namespace FoodApp.DTOs.Orders
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string? ApplicationUserId { get; set; }
        public int RestaurantId { get; set; }
        public int AddressId { get; set; }
        public DateTime CreatedAt { get; set; }
        public double TotalAmount { get; set; }
        public string? Status { get; set; } 
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}