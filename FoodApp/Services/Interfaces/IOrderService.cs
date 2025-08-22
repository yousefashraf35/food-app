using FoodApp.DTOs.Orders;
using FoodApp.Models;

namespace FoodApp.Services.Interfaces
{
    public interface IOrderService
    {

        Task<OrderDto> CreateOrderAsync(CreateOrderDto Dto);
        Task<List<OrderDto>> GetOrdersByUserIdAsync(string userId);
        Task<List<OrderDto>> GetOrdersByRestaurantIdAsync(int restaurantId);
        Task UpdateOrderStatusAsync(UpdateOrderStatusDto dto);
    
    }
}