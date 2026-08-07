using TownBites.AdminWeb.Models.Menu;
using TownBites.AdminWeb.Models.Category;

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

public interface IMenuAdminService
{
    Task<List<MenuItemModel>> GetAllAsync();

    Task<MenuItemModel?> GetByIdAsync(int id);

    Task<bool> SaveAsync(MenuItemModel model);

    Task<bool> DeleteAsync(int id);

    Task<bool> ToggleAvailabilityAsync(int id);

    Task<List<CategoryModel>> GetCategoriesAsync();
}