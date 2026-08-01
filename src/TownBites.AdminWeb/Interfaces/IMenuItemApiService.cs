using TownBites.AdminWeb.Models.Menu;

namespace TownBites.AdminWeb.Interfaces;

public interface IMenuItemApiService
{
    Task<List<MenuItemDto>> GetAllAsync();

    Task<MenuItemDto?> GetByIdAsync(int id);

    Task CreateAsync(CreateMenuItemRequest request);

    Task UpdateAsync(UpdateMenuItemRequest request);

    Task DeleteAsync(int id);

    Task<List<CategoryLookupDto>> GetCategoriesAsync();
}