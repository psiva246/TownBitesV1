using TownBites.Domain.Entities;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.Infrastructure.Interfaces;

public interface IMenuItemService
{
    Task<ApiResponse<List<MenuItemDto>>> GetAllAsync();

    Task<ApiResponse<MenuItemDto>> GetByIdAsync(int id);

    Task<ApiResponse<List<MenuItemDto>>> GetByCategoryAsync(int categoryId);

    Task<ApiResponse<List<MenuItemDto>>> GetByRestaurantAsync(int restaurantId);

    Task<ApiResponse<MenuItemDto>> CreateAsync(CreateMenuItemRequest request);

    Task<ApiResponse<MenuItemDto>> UpdateAsync(int id, UpdateMenuItemRequest request);

    Task<ApiResponse<bool>> DeleteAsync(int id);

    Task<ApiResponse<bool>> ChangeAvailabilityAsync(int id, bool isAvailable);
}