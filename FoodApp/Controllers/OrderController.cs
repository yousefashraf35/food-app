using Microsoft.AspNetCore.Mvc;
using FoodApp.DTOs.Orders;
using FoodApp.Services.Interfaces;

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
        [HttpGet("test")]
        public IActionResult Test() => Ok("Order API is working!");
        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderService.CreateOrderAsync(dto);
            return Ok(order);
        }

        // GET: api/order/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(string userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            if (orders == null || !orders.Any())
                return NotFound("No orders found for this user.");

            return Ok(orders);
        }

        // GET: api/order/restaurant/{restaurantId}
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetOrdersByRestaurant(int restaurantId)
        {
            var orders = await _orderService.GetOrdersByRestaurantIdAsync(restaurantId);
            if (orders == null || !orders.Any())
                return NotFound("No orders found for this restaurant.");

            return Ok(orders);
        }

        // PUT: api/order/status
        [HttpPut("status")]
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
