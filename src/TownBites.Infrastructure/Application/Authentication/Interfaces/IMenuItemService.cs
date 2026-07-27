using TownBites.Domain.Entities;
using TownBites.Shared.Contracts.Requests;

namespace TownBites.Infrastructure.Interfaces;

public interface IMenuItemService
{
    Task<MenuItem> CreateAsync(
        int categoryId,
        CreateMenuItemRequest request);

    Task<List<MenuItem>> GetByCategoryAsync(int categoryId);

    Task<MenuItem?> GetByIdAsync(int id);

    Task<MenuItem?> UpdateAsync(
        int id,
        UpdateMenuItemRequest request);

    Task<bool> DeleteAsync(int id);
}