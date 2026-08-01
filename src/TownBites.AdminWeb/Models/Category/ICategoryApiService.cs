using TownBites.AdminWeb.Models.Category;

namespace TownBites.AdminWeb.Interfaces;

public interface ICategoryApiService
{
    Task<List<CategoryDto>> GetAllAsync();

    Task<CategoryDto?> GetByIdAsync(int id);

    Task CreateAsync(CreateCategoryRequest request);

    Task UpdateAsync(UpdateCategoryRequest request);

    Task DeleteAsync(int id);
}