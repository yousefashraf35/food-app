using FoodApp.Data;
using FoodApp.DTOs.MenuItems;
using FoodApp.DTOs.Restaurants;
using FoodApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class RestaurantService : IRestaurantService
{
    private readonly ApplicationDbContext _context;
    public RestaurantService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<RestaurantDto> CreateRestaurantAsync(RestaurantCreationDto dto)
    {
        Restaurant restaurant = new Restaurant
        {
            Name = dto.Name,
            CuisineType = dto.CuisineType,
            DeliveryRadius = dto.DeliveryRadius,
            PhoneNumber = dto.PhoneNumber,
            Location = dto.Location
        };
        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();
        return new RestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            CuisineType = restaurant.CuisineType,
            DeliveryRadius = restaurant.DeliveryRadius,
            PhoneNumber = restaurant.PhoneNumber,
            Location = restaurant.Location
        };
    }

    public async Task<List<RestaurantDto>> GetAllRestaurantsAsync()
    {
        List<RestaurantDto> restaurants = await _context.Restaurants.Select(r => new RestaurantDto
        {
            Id = r.Id,
            Name = r.Name,
            CuisineType = r.CuisineType,
            DeliveryRadius = r.DeliveryRadius,
            PhoneNumber = r.PhoneNumber,
            Location = r.Location
        }).ToListAsync();

        return restaurants;
    }

    public async Task<RestaurantDto> GetRestaurantByIdAsync(int id)
    {
        Restaurant? restaurant = await _context.Restaurants.FindAsync(id);
        if (restaurant == null)
            throw new KeyNotFoundException($"Restaurant with id {id} not found.");
        return new RestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            CuisineType = restaurant.CuisineType,
            DeliveryRadius = restaurant.DeliveryRadius,
            PhoneNumber = restaurant.PhoneNumber,
            Location = restaurant.Location
        };
    }

    public async Task DeleteRestaurantAsync(int id)
    {
        Restaurant? restaurant = await _context.Restaurants.FindAsync(id);
        if (restaurant == null)
            throw new KeyNotFoundException($"Restaurant with id {id} not found.");
        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task<MenuItemDto> AddMenuItemToRestaurantAsync(int restaurantId, MenuItemCreationDto dto)
    {
        Category? category = null;
        if (!string.IsNullOrWhiteSpace(dto.CategoryName))
        {
            category = await _context.Categories
                .SingleOrDefaultAsync(c => c.Name == dto.CategoryName);
            if (category == null)
            {
                category = new Category { Name = dto.CategoryName };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
        }

        Restaurant? restaurant = await _context.Restaurants.FindAsync(restaurantId);
        if (restaurant == null)
            throw new KeyNotFoundException($"Restaurant with id {restaurantId} not found.");

        MenuItem menuItem = new MenuItem
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            IsAvailable = dto.IsAvailable,
            Category = category,
            Restaurant = restaurant,
            RestaurantId = restaurant.Id
        };
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();
        
        return new MenuItemDto
        {
            Id = menuItem.Id,
            Name = menuItem.Name,
            Description = menuItem.Description,
            Price = menuItem.Price,
            IsAvailable = menuItem.IsAvailable,
            CategoryId = menuItem.CategoryId,
            RestaurantId = menuItem.RestaurantId
        };
    }

    public async Task<List<MenuItemDto>> GetAllRestaurantMenuItems(int restaurantId)
    {
        return await _context.MenuItems
            .Where(mi => mi.RestaurantId == restaurantId)
            .Select(mi => new MenuItemDto
            {
                Id = mi.Id,
                Name = mi.Name,
                Description = mi.Description,
                Price = mi.Price,
                IsAvailable = mi.IsAvailable,
                CategoryId = mi.CategoryId,
                RestaurantId = mi.RestaurantId
            })
            .ToListAsync();
    }
}