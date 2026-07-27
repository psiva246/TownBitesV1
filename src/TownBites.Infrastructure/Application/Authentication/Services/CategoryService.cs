using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Requests;

namespace TownBites.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> CreateAsync(
        int restaurantId,
        CreateCategoryRequest request)
    {
        var restaurant = await _dbContext.Restaurants
            .FirstOrDefaultAsync(x => x.Id == restaurantId && x.IsActive);

        if (restaurant == null)
            throw new Exception("Restaurant not found.");

        var category = new Category
        {
            RestaurantId = restaurantId,
            Name = request.Name,
            DisplayOrder = request.DisplayOrder,
            IsActive = true
        };

        _dbContext.Categories.Add(category);

        await _dbContext.SaveChangesAsync();

        return category;
    }

    public async Task<List<Category>> GetByRestaurantAsync(int restaurantId)
    {
        return await _dbContext.Categories
            .Where(x => x.RestaurantId == restaurantId && x.IsActive)
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Category?> UpdateAsync(
        int id,
        UpdateCategoryRequest request)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return null;

        category.Name = request.Name;
        category.DisplayOrder = request.DisplayOrder;
        category.IsActive = request.IsActive;

        await _dbContext.SaveChangesAsync();

        return category;
    }

    public async Task<bool> DeactivateAsync(int id)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            return false;

        category.IsActive = false;

        await _dbContext.SaveChangesAsync();

        return true;
    }
}