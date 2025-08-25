using FoodApp.DTOs.Restaurants;
using FoodApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateRestaurant([FromBody] RestaurantCreationDto dto)
        {
            var createdRestaurant = await _restaurantService.CreateRestaurantAsync(dto);
            return StatusCode(StatusCodes.Status201Created, createdRestaurant);
        }
        [HttpGet]
        [Authorize(Roles = "Customer, Manager")]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Customer, Manager")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
                return Ok(restaurant);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteRestaurantById(int id)
        {
            try
            {
                await _restaurantService.DeleteRestaurantAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            } 
        }
    }
}