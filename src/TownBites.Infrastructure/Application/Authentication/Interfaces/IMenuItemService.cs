using TownBites.Domain.Entities;
using TownBites.Shared.Contracts.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.Infrastructure.Interfaces;

public interface IMenuItemService
{
    Task<MenuItem> CreateAsync(int categoryId, CreateMenuItemRequest request);

    Task<List<MenuItem>> GetByCategoryAsync(int categoryId);

    Task<MenuItem?> GetByIdAsync(int id);

    Task<PagedResponse<MenuItemResponse>> GetAllAsync(int restaurantId, PaginationRequest request);

    Task<MenuItem?> UpdateAsync(int id, UpdateMenuItemRequest request);

    Task<bool> DeleteAsync(int id);
}