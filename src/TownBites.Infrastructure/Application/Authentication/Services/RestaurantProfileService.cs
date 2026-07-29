using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.Infrastructure.Services;

public class RestaurantProfileService : IRestaurantProfileService
{
    private readonly ApplicationDbContext _dbContext;

    public RestaurantProfileService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RestaurantProfileResponse?> GetAsync(int restaurantId)
    {
        return await _dbContext.Restaurants
            .AsNoTracking()
            .Where(r => r.Id == restaurantId && r.IsActive)
            .Select(r => new RestaurantProfileResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                LogoUrl = r.LogoUrl,
                CoverImageUrl = r.CoverImageUrl,
                OpeningTime = r.OpeningTime,
                ClosingTime = r.ClosingTime,
                DeliveryCharge = r.DeliveryCharge,
                MinimumOrderAmount = r.MinimumOrderAmount,
                DeliveryRadiusInKm = r.DeliveryRadiusInKm,
                EstimatedDeliveryMinutes = r.EstimatedDeliveryMinutes,
                IsOpen = r.IsOpen
            })
            .FirstOrDefaultAsync();
    }

    public async Task<RestaurantProfileResponse?> UpdateAsync(int restaurantId, UpdateRestaurantProfileRequest request)
    {
        var restaurant = await _dbContext.Restaurants
            .FirstOrDefaultAsync(r =>
                r.Id == restaurantId &&
                r.IsActive);

        if (restaurant == null)
            return null;

        restaurant.Name = request.Name;
        restaurant.Description = request.Description;
        restaurant.LogoUrl = request.LogoUrl;
        restaurant.CoverImageUrl = request.CoverImageUrl;
        restaurant.OpeningTime = request.OpeningTime;
        restaurant.ClosingTime = request.ClosingTime;
        restaurant.DeliveryCharge = request.DeliveryCharge;
        restaurant.MinimumOrderAmount = request.MinimumOrderAmount;
        restaurant.DeliveryRadiusInKm = request.DeliveryRadiusInKm;
        restaurant.EstimatedDeliveryMinutes = request.EstimatedDeliveryMinutes;
        restaurant.IsOpen = request.IsOpen;

        await _dbContext.SaveChangesAsync();

        return Map(restaurant);
    }

    private static RestaurantProfileResponse Map(Restaurant restaurant)
    {
        return new RestaurantProfileResponse
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Description = restaurant.Description,
            LogoUrl = restaurant.LogoUrl,
            CoverImageUrl = restaurant.CoverImageUrl,
            OpeningTime = restaurant.OpeningTime,
            ClosingTime = restaurant.ClosingTime,
            DeliveryCharge = restaurant.DeliveryCharge,
            MinimumOrderAmount = restaurant.MinimumOrderAmount,
            DeliveryRadiusInKm = restaurant.DeliveryRadiusInKm,
            EstimatedDeliveryMinutes = restaurant.EstimatedDeliveryMinutes,
            IsOpen = restaurant.IsOpen
        };
    }
}