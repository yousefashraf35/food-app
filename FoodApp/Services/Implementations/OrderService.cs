using Microsoft.EntityFrameworkCore;
using FoodApp.Data;
using FoodApp.Models;
using FoodApp.DTOs.Orders;
using FoodApp.Services.Interfaces;

namespace FoodApp.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                ApplicationUserId = dto.ApplicationUserId,
                RestaurantId = dto.RestaurantId,
                AddressId = dto.AddressId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                TotalAmount = dto.Items.Sum(item => item.Price * item.Quantity),
                OrderItems= dto.Items.Select(item => new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
                
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OrderDto{
                OrderId = order.OrderId,
                ApplicationUserId = order.ApplicationUserId,
                RestaurantId = order.RestaurantId,
                AddressId = order.AddressId,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };
        }
        public async Task<List<OrderDto>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _context.Orders 
                .Include(o => o.OrderItems)
                .Where(o => o.ApplicationUserId == userId)
                .ToListAsync();

            if (!orders.Any())
                return new List<OrderDto>();

            return orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                ApplicationUserId = o.ApplicationUserId,
                RestaurantId = o.RestaurantId,
                AddressId = o.AddressId,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                TotalAmount = o.TotalAmount,
                Items = o.OrderItems.Select(oi => new OrderItemDto
                {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            }).ToList();
        }

        public async Task<List<OrderDto>> GetOrdersByRestaurantIdAsync(int restaurantId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.RestaurantId == restaurantId)
                .ToListAsync();

            return orders.Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                ApplicationUserId = o.ApplicationUserId,
                RestaurantId = o.RestaurantId,
                AddressId = o.AddressId,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                TotalAmount = o.TotalAmount,
                Items = o.OrderItems.Select(oi => new OrderItemDto
                {
                    MenuItemId = oi.MenuItemId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            }).ToList();
        }
        public async Task UpdateOrderStatusAsync(UpdateOrderStatusDto dto)
        {
            var order = await _context.Orders.FindAsync(dto.OrderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            order.Status = dto.Status;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}