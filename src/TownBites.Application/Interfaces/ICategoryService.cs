using TownBites.Application.DTOs;
using TownBites.Application.Requests;
//using TownBites.Shared.Contracts.Requests;

namespace TownBites.Application.Interfaces;

public interface ICategoryService
{
    Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllAsync(int restaurantId);

    Task<ApiResponse<CategoryDto>> GetByIdAsync(int id);

    Task<ApiResponse<CategoryDto>> CreateAsync(CreateCategoryRequest request);
    Task<ApiResponse<bool>> UpdateAsync(int id, UpdateCategoryRequest request);

    Task<ApiResponse<bool>> DeleteAsync(int id);
}