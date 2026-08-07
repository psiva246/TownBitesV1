using TownBites.Application.DTOs;
using TownBites.Application.Requests;
//using TownBites.Shared.Contracts.Requests;

namespace TownBites.Application.Interfaces;

public interface IRestaurantService
{
    Task<ApiResponse<IEnumerable<RestaurantDto>>> GetAllAsync();

    Task<ApiResponse<RestaurantDto>> GetByIdAsync(int id);

    Task<ApiResponse<RestaurantDto>> CreateAsync(CreateRestaurantRequest request);

    Task<ApiResponse<RestaurantDto>> UpdateAsync(
        int id,
        UpdateRestaurantRequest request);

    Task<ApiResponse<bool>> DeleteAsync(int id);
}