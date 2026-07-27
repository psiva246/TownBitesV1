using TownBites.Domain.Entities;
using TownBites.Shared.Contracts.Requests;

namespace TownBites.Infrastructure.Interfaces;

public interface ICategoryService
{
    Task<Category> CreateAsync(
        int restaurantId,
        CreateCategoryRequest request);

    Task<List<Category>> GetByRestaurantAsync(int restaurantId);

    Task<Category?> UpdateAsync(
        int id,
        UpdateCategoryRequest request);

    Task<bool> DeactivateAsync(int id);
}