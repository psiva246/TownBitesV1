using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Requests;

namespace TownBites.Infrastructure.Services;

public class RestaurantService : IRestaurantService
{
    private readonly ApplicationDbContext _dbContext;

    public RestaurantService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Restaurant> CreateAsync(CreateRestaurantRequest request)
    {
        var restaurant = new Restaurant
        {
            Name = request.Name,
            OwnerName = request.OwnerName,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            IsOpen = true,
            IsActive = true
        };

        _dbContext.Restaurants.Add(restaurant);

        await _dbContext.SaveChangesAsync();

        return restaurant;
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await _dbContext.Restaurants
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await _dbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
    }

    public async Task<Restaurant?> UpdateAsync(
    int id,
    UpdateRestaurantRequest request)
    {
        var restaurant = await _dbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

        if (restaurant == null)
            return null;

        restaurant.Name = request.Name;
        restaurant.OwnerName = request.OwnerName;
        restaurant.PhoneNumber = request.PhoneNumber;
        restaurant.Address = request.Address;
        restaurant.IsOpen = request.IsOpen;

        await _dbContext.SaveChangesAsync();

        return restaurant;
    }

    public async Task<bool> DeactivateAsync(int id)
    {
        var restaurant = await _dbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

        if (restaurant == null)
            return false;

        restaurant.IsActive = false;

        await _dbContext.SaveChangesAsync();

        return true;
    }
}