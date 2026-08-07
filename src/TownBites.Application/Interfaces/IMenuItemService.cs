using TownBites.Application.DTOs;
using TownBites.Application.Requests;
//using TownBites.Shared.Contracts.Requests;

namespace TownBites.Application.Interfaces;

public interface IMenuItemService
{
    Task<ApiResponse<IEnumerable<MenuItemDto>>> GetAllAsync();

    Task<ApiResponse<IEnumerable<MenuItemDto>>> GetByRestaurantAsync(int restaurantId);

    Task<ApiResponse<MenuItemDto>> GetByIdAsync(int id);

    Task<ApiResponse<MenuItemDto>> CreateAsync(CreateMenuItemRequest request);

    Task<ApiResponse<MenuItemDto>> UpdateAsync(
        int id,
        UpdateMenuItemRequest request);

    Task<ApiResponse<bool>> DeleteAsync(int id);
}