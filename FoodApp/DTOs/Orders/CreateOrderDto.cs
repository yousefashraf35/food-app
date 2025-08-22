namespace FoodApp.DTOs.Orders
{
    public class CreateOrderDto
    {
        public string ApplicationUserId { get; set; }
        public int RestaurantId { get; set; }
        public int AddressId { get; set; }

        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();

    }
}