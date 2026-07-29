using TownBites.Domain.Entities;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.Infrastructure.Interfaces;

public interface IRestaurantProfileService
{
    Task<RestaurantProfileResponse?> GetAsync(int restaurantId);

    Task<RestaurantProfileResponse?> UpdateAsync(int restaurantId, UpdateRestaurantProfileRequest request);
}