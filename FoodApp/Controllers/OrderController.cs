using Microsoft.AspNetCore.Mvc;
using FoodApp.DTOs.Orders;
using FoodApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FoodApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST: api/order
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderService.CreateOrderAsync(dto);
            return Ok(order);
        }

        // GET: api/order/user/{userId}
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Customer, Manager")]
        public async Task<IActionResult> GetOrdersByUser(string userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            if (orders == null || !orders.Any())
                return NotFound("No orders found for this user.");

            return Ok(orders);
        }

        // GET: api/order/restaurant/{restaurantId}
        [HttpGet("restaurant/{restaurantId}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetOrdersByRestaurant(int restaurantId)
        {
            var orders = await _orderService.GetOrdersByRestaurantIdAsync(restaurantId);
            if (orders == null || !orders.Any())
                return NotFound("No orders found for this restaurant.");

            return Ok(orders);
        }

        // PUT: api/order/status
        [HttpPut("status")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _orderService.UpdateOrderStatusAsync(dto);
                return Ok("Order status updated successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
