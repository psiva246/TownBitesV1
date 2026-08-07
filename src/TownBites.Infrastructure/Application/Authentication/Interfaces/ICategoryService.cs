using TownBites.Domain.Entities;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Models;

namespace TownBites.Infrastructure.Interfaces;

public interface ICategoryService
{
    //Task<Category> CreateAsync(
    //    int restaurantId,
    //    CreateCategoryRequest request);

    //Task<List<Category>> GetByRestaurantAsync(int restaurantId);

    //Task<Category?> UpdateAsync(
    //    int id,
    //    UpdateCategoryRequest request);

    //Task<bool> DeactivateAsync(int id);

    Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllAsync(int restaurantId);

    Task<ApiResponse<CategoryDto>> GetByIdAsync(int id);

    Task<ApiResponse<CategoryDto>> CreateAsync(CreateCategoryRequest request);
    Task<ApiResponse<CategoryDto>> UpdateAsync(int id, UpdateCategoryRequest request);

    Task<ApiResponse<bool>> DeleteAsync(int id);
}