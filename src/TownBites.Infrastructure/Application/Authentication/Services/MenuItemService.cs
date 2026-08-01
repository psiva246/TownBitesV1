using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TownBites.Infrastructure.Services;

public class MenuItemService : IMenuItemService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IAuditService _auditService;

    public MenuItemService(ApplicationDbContext dbContext, IAuditService auditService)
    {
        _dbContext = dbContext;
        _auditService = auditService;
    }

    public async Task<MenuItem> CreateAsync(int categoryId, CreateMenuItemRequest request)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId && x.IsActive);

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

        await _auditService.LogAsync("1", "Siva", "Create", "MenuItem", menuItem.Id, null, menuItem);

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

    public async Task<PagedResponse<MenuItemResponse>> GetAllAsync(int restaurantId, PaginationRequest request)
    {
        IQueryable<MenuItem> query = _dbContext.MenuItems.Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(x => x.Name.Contains(request.Search));
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new MenuItemResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId,
                IsAvailable = x.IsAvailable
            })
            .ToListAsync();

        return new PagedResponse<MenuItemResponse>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = total
        };
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _dbContext.MenuItems
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
    }

    public async Task<MenuItem?> UpdateAsync(int id, UpdateMenuItemRequest request)
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