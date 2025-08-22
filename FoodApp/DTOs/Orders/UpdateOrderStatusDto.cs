namespace FoodApp.DTOs.Orders
{
    public class UpdateOrderStatusDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; } // e.g., "Pending", "In Progress", "Completed", etc.
    }
}