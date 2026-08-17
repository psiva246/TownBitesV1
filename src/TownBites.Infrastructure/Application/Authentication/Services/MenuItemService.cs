using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TownBites.Infrastructure.Services;

public class MenuItemService : IMenuItemService
{
    private readonly ApplicationDbContext _context;

    public MenuItemService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<MenuItemDto>>> GetAllAsync()
    {
        var items = await _context.MenuItems
            .Include(x => x.Category)
            .OrderBy(x => x.Name)
            .Select(x => new MenuItemDto
            {
                Id = x.Id,
                RestaurantId = x.RestaurantId,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                DiscountPrice = x.DiscountPrice?? 0,
                ImageUrl = x.ImageUrl,
                IsAvailable = x.IsAvailable,
                IsVeg = x.IsVeg
            })
            .ToListAsync();

        return new ApiResponse<List<MenuItemDto>>
        {
            Success = true,
            Message = "Menu items retrieved successfully.",
            Data = items
        };
    }

    public async Task<ApiResponse<MenuItemDto>> GetByIdAsync(int id)
    {
        var item = await _context.MenuItems
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
        {
            return new ApiResponse<MenuItemDto>
            {
                Success = false,
                Message = "Menu item not found."
            };
        }

        return new ApiResponse<MenuItemDto>
        {
            Success = true,
            Message = "Menu item retrieved successfully.",
            Data = new MenuItemDto
            {
                Id = item.Id,
                RestaurantId = item.RestaurantId,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                DiscountPrice = item.DiscountPrice ?? 0,
                existingImageUrl = item.ImageUrl,
                ImageUrl = item.ImageUrl,
                IsAvailable = item.IsAvailable,
                IsVeg = item.IsVeg
            }
        };
    }

    public async Task<ApiResponse<List<MenuItemDto>>> GetByCategoryAsync(int categoryId)
    {
        var items = await _context.MenuItems
            .Include(x => x.Category)
            .Where(x => x.CategoryId == categoryId)
            .Select(x => new MenuItemDto
            {
                Id = x.Id,
                RestaurantId = x.RestaurantId,
                CategoryId = x.CategoryId,
                //CategoryName = x.Category.Name,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                IsAvailable = x.IsAvailable
            })
            .ToListAsync();

        return new ApiResponse<List<MenuItemDto>>
        {
            Success = true,
            Message = "Menu items retrieved successfully.",
            Data = items
        };
    }

    public async Task<ApiResponse<List<MenuItemDto>>> GetByRestaurantAsync(int restaurantId)
    {
        var items = await _context.MenuItems
            .Include(x => x.Category)
            .Where(x => x.RestaurantId == restaurantId)
            .Select(x => new MenuItemDto
            {
                Id = x.Id,
                RestaurantId = x.RestaurantId,
                CategoryId = x.CategoryId,
                //CategoryName = x.Category.Name,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                IsAvailable = x.IsAvailable
            })
            .ToListAsync();

        return new ApiResponse<List<MenuItemDto>>
        {
            Success = true,
            Message = "Menu items retrieved successfully.",
            Data = items
        };
    }

    public async Task<ApiResponse<MenuItemDto>> CreateAsync(CreateMenuItemRequest request)
    {
        var entity = new MenuItem
        {
            //RestaurantId = request.RestaurantId,
            //CategoryId = request.CategoryId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ImageUrl = request.ImageUrl,
            IsAvailable = request.IsAvailable
        };

        _context.MenuItems.Add(entity);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(entity.Id);
    }

    public async Task<ApiResponse<MenuItemDto>> UpdateAsync(int id, UpdateMenuItemRequest request)
    {
        var entity = await _context.MenuItems.FindAsync(id);

        if (entity == null)
        {
            return new ApiResponse<MenuItemDto>
            {
                Success = false,
                Message = "Menu item not found."
            };
        }

        entity.RestaurantId = request.RestaurantId;
        entity.CategoryId = request.CategoryId;
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.DiscountPrice = request.DiscountPrice;
        entity.ImageUrl = request.ImageUrl ?? entity.ImageUrl;
        entity.IsAvailable = request.IsAvailable;
        entity.IsVeg = request.IsVeg;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var entity = await _context.MenuItems.FindAsync(id);

        if (entity == null)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Menu item not found.",
                Data = false
            };
        }

        _context.MenuItems.Remove(entity);

        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Menu item deleted successfully.",
            Data = true
        };
    }

    public async Task<ApiResponse<bool>> ChangeAvailabilityAsync(int id, bool isAvailable)
    {
        var entity = await _context.MenuItems.FindAsync(id);

        if (entity == null)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Menu item not found.",
                Data = false
            };
        }

        entity.IsAvailable = isAvailable;

        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Availability updated successfully.",
            Data = true
        };
    }
}