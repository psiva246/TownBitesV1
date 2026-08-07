using Microsoft.EntityFrameworkCore;
using TownBites.Application.DTOs;
using TownBites.Application.Interfaces;
using TownBites.Application.Requests;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;

namespace TownBites.Application.Services;

public class MenuItemService : IMenuItemService
{
    private readonly ApplicationDbContext _context;

    public MenuItemService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IEnumerable<MenuItemDto>>> GetAllAsync()
    {
        var items = await _context.MenuItems
            .OrderBy(x => x.Name)
            .Select(x => new MenuItemDto
            {
                Id = x.Id,
                RestaurantId = x.RestaurantId,
                CategoryId = x.CategoryId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                IsAvailable = x.IsAvailable
            })
            .ToListAsync();

        return new ApiResponse<IEnumerable<MenuItemDto>>
        {
            Success = true,
            Message = "Menu items retrieved successfully.",
            Data = items
        };
    }

    public async Task<ApiResponse<IEnumerable<MenuItemDto>>> GetByRestaurantAsync(int restaurantId)
    {
        var items = await _context.MenuItems
            .Where(x => x.RestaurantId == restaurantId)
            .OrderBy(x => x.Name)
            .Select(x => new MenuItemDto
            {
                Id = x.Id,
                RestaurantId = x.RestaurantId,
                CategoryId = x.CategoryId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                IsAvailable = x.IsAvailable
            })
            .ToListAsync();

        return new ApiResponse<IEnumerable<MenuItemDto>>
        {
            Success = true,
            Message = "Menu items retrieved successfully.",
            Data = items
        };
    }

    public async Task<ApiResponse<MenuItemDto>> GetByIdAsync(int id)
    {
        var item = await _context.MenuItems
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
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                ImageUrl = item.ImageUrl,
                IsAvailable = item.IsAvailable
            }
        };
    }

    public async Task<ApiResponse<MenuItemDto>> CreateAsync(CreateMenuItemRequest request)
    {
        var menuItem = new MenuItem
        {
            RestaurantId = request.RestaurantId,
            CategoryId = request.CategoryId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ImageUrl = request.ImageUrl,
            IsAvailable = true
        };

        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();

        return new ApiResponse<MenuItemDto>
        {
            Success = true,
            Message = "Menu item created successfully.",
            Data = new MenuItemDto
            {
                Id = menuItem.Id,
                RestaurantId = menuItem.RestaurantId,
                CategoryId = menuItem.CategoryId,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                ImageUrl = menuItem.ImageUrl,
                IsAvailable = menuItem.IsAvailable
            }
        };
    }

    public async Task<ApiResponse<MenuItemDto>> UpdateAsync(int id, UpdateMenuItemRequest request)
    {
        var item = await _context.MenuItems
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
        {
            return new ApiResponse<MenuItemDto>
            {
                Success = false,
                Message = "Menu item not found."
            };
        }

        item.CategoryId = request.CategoryId;
        item.Name = request.Name;
        item.Description = request.Description;
        item.Price = request.Price;
        item.ImageUrl = request.ImageUrl;
        item.IsAvailable = request.IsAvailable;

        _context.MenuItems.Update(item);
        await _context.SaveChangesAsync();

        return new ApiResponse<MenuItemDto>
        {
            Success = true,
            Message = "Menu item updated successfully.",
            Data = new MenuItemDto
            {
                Id = item.Id,
                RestaurantId = item.RestaurantId,
                CategoryId = item.CategoryId,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                ImageUrl = item.ImageUrl,
                IsAvailable = item.IsAvailable
            }
        };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var item = await _context.MenuItems
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Menu item not found.",
                Data = false
            };
        }

        _context.MenuItems.Remove(item);
        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Menu item deleted successfully.",
            Data = true
        };
    }
}