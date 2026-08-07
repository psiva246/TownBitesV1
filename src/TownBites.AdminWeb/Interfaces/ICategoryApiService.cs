using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Common;

namespace TownBites.AdminWeb.Interfaces
{
    public interface ICategoryApiService
    {
        Task<ApiResponse<List<CategoryResponse>>> GetAllAsync(int restaurantId);

        Task<ApiResponse<CategoryResponse>> GetByIdAsync(int id);

        Task<ApiResponse<int>> CreateAsync(CategoryRequest request);

        Task<ApiResponse<bool>> UpdateAsync(int id, CategoryRequest request);

        Task<ApiResponse<bool>> DeleteAsync(int id);        
    }
    
}