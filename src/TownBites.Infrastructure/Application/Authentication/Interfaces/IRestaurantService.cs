using TownBites.Domain.Entities;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.Infrastructure.Interfaces;

public interface IRestaurantService
{
    Task<Restaurant> CreateAsync(CreateRestaurantRequest request);

    Task<List<Restaurant>> GetAllAsync();

    Task<Restaurant?> GetByIdAsync(int id);

    Task<Restaurant?> UpdateAsync(int id, UpdateRestaurantRequest request);

    Task<bool> DeactivateAsync(int id);
}