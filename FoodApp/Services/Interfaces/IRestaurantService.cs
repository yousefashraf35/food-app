using FoodApp.DTOs.Restaurants;

namespace FoodApp.Services.Interfaces;
public interface IRestaurantService
{
    Task<RestaurantDto> CreateRestaurantAsync(RestaurantCreationDto dto);
    Task<List<RestaurantDto>> GetAllRestaurantsAsync();
    Task<RestaurantDto> GetRestaurantByIdAsync(int id);
    // add menu item to restaurant
    Task DeleteRestaurantAsync(int id);
}