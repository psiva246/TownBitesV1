using TownBites.AdminWeb.Models.Menu;
using TownBites.AdminWeb.Models.Category;
//using TownBites.Application.DTOs;

namespace TownBites.AdminWeb.Interfaces
{
    public interface IMenuItemApiService
    {
        //Task<List<MenuItemDto>> GetAllAsync();

        //Task<MenuItemDto?> GetByIdAsync(int id);

        //Task CreateAsync(CreateMenuItemRequest request);

        //Task UpdateAsync(UpdateMenuItemRequest request);

        //Task DeleteAsync(int id);

        Task<List<CategoryLookupDto>> GetCategoriesAsync();

        Task<TownBites.Application.DTOs.ApiResponse<List<MenuItemDto>>> GetAllAsync();

        Task<TownBites.Application.DTOs.ApiResponse<MenuItemDto>> GetByIdAsync(int id);

        Task<TownBites.Application.DTOs.ApiResponse<MenuItemDto>> CreateAsync(MenuItemRequest request);

        Task<TownBites.Application.DTOs.ApiResponse<MenuItemDto>> UpdateAsync(int id, MenuItemRequest request);

        Task<TownBites.Application.DTOs.ApiResponse<bool>> DeleteAsync(int id);
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
}