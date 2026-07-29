using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TownBites.Infrastructure.Data;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.API.Controllers;

[ApiController]
[Route("api/customer")]
//[Authorize(Roles = "Customer")]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Returns all active restaurants.
    /// </summary>
    [HttpGet("restaurants")]
    public async Task<IActionResult> GetRestaurants()
    {
        var restaurants = await _dbContext.Restaurants
            .AsNoTracking()
            .Where(r => r.IsActive)
            .OrderBy(r => r.Name)
            .Select(r => new RestaurantResponse
            {
                Id = r.Id,
                Name = r.Name,
                OwnerName = r.OwnerName,
                PhoneNumber = r.PhoneNumber,
                Address = r.Address,
                IsOpen = r.IsOpen,
                IsActive = r.IsActive
            })
            .ToListAsync();

        return Ok(ApiResponse<List<RestaurantResponse>>.Ok(restaurants));
    }

    /// <summary>
    /// Returns categories of a restaurant.
    /// </summary>
    [HttpGet("restaurants/{restaurantId:int}/categories")]
    public async Task<IActionResult> GetCategories(int restaurantId)
    {
        var categories = await _dbContext.Categories
            .AsNoTracking()
            .Where(c => c.RestaurantId == restaurantId && c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .Select(c => new CategoryResponse
            {
                Id = c.Id,
                RestaurantId = c.RestaurantId,
                Name = c.Name,
                DisplayOrder = c.DisplayOrder,
                IsActive = c.IsActive
            })
            .ToListAsync();

        return Ok(ApiResponse<List<CategoryResponse>>.Ok(categories));
    }

    /// <summary>
    /// Returns menu items of a category.
    /// </summary>
    [HttpGet("categories/{categoryId:int}/menu-items")]
    public async Task<IActionResult> GetMenuItems(int categoryId)
    {
        var items = await _dbContext.MenuItems
            .AsNoTracking()
            .Where(m => m.CategoryId == categoryId &&
                        m.IsActive &&
                        m.IsAvailable)
            .OrderBy(m => m.Name)
            .Select(m => new MenuItemResponse
            {
                Id = m.Id,
                CategoryId = m.CategoryId,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                DiscountPrice = m.DiscountPrice,
                IsVeg = m.IsVeg,
                IsAvailable = m.IsAvailable,
                PreparationTimeInMinutes = m.PreparationTimeInMinutes,
                ImageUrl = m.ImageUrl
            })
            .ToListAsync();

        return Ok(ApiResponse<List<MenuItemResponse>>.Ok(items));
    }
}