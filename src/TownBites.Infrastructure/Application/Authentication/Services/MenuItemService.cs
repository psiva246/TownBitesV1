using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Requests;

namespace TownBites.Infrastructure.Services;

public class MenuItemService : IMenuItemService
{
    private readonly ApplicationDbContext _dbContext;

    public MenuItemService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MenuItem> CreateAsync(
        int categoryId,
        CreateMenuItemRequest request)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == categoryId && x.IsActive);

        if (category == null)
            throw new Exception("Category not found.");

        var menuItem = new MenuItem
        {
            CategoryId = categoryId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            DiscountPrice = request.DiscountPrice,
            IsVeg = request.IsVeg,
            IsAvailable = request.IsAvailable,
            PreparationTimeInMinutes = request.PreparationTimeInMinutes,
            ImageUrl = request.ImageUrl,
            IsActive = true
        };

        _dbContext.MenuItems.Add(menuItem);

        await _dbContext.SaveChangesAsync();

        return menuItem;
    }

    public async Task<List<MenuItem>> GetByCategoryAsync(int categoryId)
    {
        return await _dbContext.MenuItems
            .Where(x =>
                x.CategoryId == categoryId &&
                x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _dbContext.MenuItems
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.IsActive);
    }

    public async Task<MenuItem?> UpdateAsync(
        int id,
        UpdateMenuItemRequest request)
    {
        var menuItem = await _dbContext.MenuItems
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.IsActive);

        if (menuItem == null)
            return null;

        menuItem.Name = request.Name;
        menuItem.Description = request.Description;
        menuItem.Price = request.Price;
        menuItem.DiscountPrice = request.DiscountPrice;
        menuItem.IsVeg = request.IsVeg;
        menuItem.IsAvailable = request.IsAvailable;
        menuItem.PreparationTimeInMinutes = request.PreparationTimeInMinutes;
        menuItem.ImageUrl = request.ImageUrl;

        await _dbContext.SaveChangesAsync();

        return menuItem;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var menuItem = await _dbContext.MenuItems
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.IsActive);

        if (menuItem == null)
            return false;

        menuItem.IsActive = false;

        await _dbContext.SaveChangesAsync();

        return true;
    }
}