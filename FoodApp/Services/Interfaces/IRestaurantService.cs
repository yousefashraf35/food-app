using FoodApp.DTOs.MenuItems;
using FoodApp.DTOs.Restaurants;

namespace FoodApp.Services.Interfaces;

public interface IRestaurantService
{
    Task<RestaurantDto> CreateRestaurantAsync(RestaurantCreationDto dto);
    Task<List<RestaurantDto>> GetAllRestaurantsAsync();
    Task<RestaurantDto> GetRestaurantByIdAsync(int id);
    Task DeleteRestaurantAsync(int id);
    Task<MenuItemDto> AddMenuItemToRestaurantAsync(int restaurantId, MenuItemCreationDto dto);
    Task<List<MenuItemDto>> GetAllMenuItems(int restaurantId);
}