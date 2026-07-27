using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.API.Controllers;

[ApiController]
//[Authorize(Roles = "Admin,Restaurant")]
[Route("api")]
public class MenuItemsController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemsController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    /// <summary>
    /// Create a menu item under a category.
    /// </summary>
    [HttpPost("categories/{categoryId:int}/menu-items")]
    public async Task<IActionResult> Create(
        int categoryId,
        [FromBody] CreateMenuItemRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));
        }

        try
        {
            var menuItem = await _menuItemService.CreateAsync(categoryId, request);

            return Ok(ApiResponse<MenuItemResponse>.Ok(
                ToResponse(menuItem),
                "Menu item created successfully."));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Get all menu items for a category.
    /// </summary>
    [HttpGet("categories/{categoryId:int}/menu-items")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        var menuItems = await _menuItemService.GetByCategoryAsync(categoryId);

        var response = menuItems
            .Select(ToResponse)
            .OrderBy(x => x.Name)
            .ToList();

        return Ok(ApiResponse<IEnumerable<MenuItemResponse>>.Ok(response));
    }

    /// <summary>
    /// Get menu item by Id.
    /// </summary>
    [HttpGet("menu-items/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var menuItem = await _menuItemService.GetByIdAsync(id);

        if (menuItem == null)
        {
            return NotFound(ApiResponse<object>.Fail("Menu item not found."));
        }

        return Ok(ApiResponse<MenuItemResponse>.Ok(ToResponse(menuItem)));
    }

    /// <summary>
    /// Update menu item.
    /// </summary>
    [HttpPut("menu-items/{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateMenuItemRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));
        }

        var menuItem = await _menuItemService.UpdateAsync(id, request);

        if (menuItem == null)
        {
            return NotFound(ApiResponse<object>.Fail("Menu item not found."));
        }

        return Ok(ApiResponse<MenuItemResponse>.Ok(
            ToResponse(menuItem),
            "Menu item updated successfully."));
    }

    /// <summary>
    /// Deactivate menu item.
    /// </summary>
    [HttpDelete("menu-items/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _menuItemService.DeleteAsync(id);

        if (!success)
        {
            return NotFound(ApiResponse<object>.Fail("Menu item not found."));
        }

        return Ok(ApiResponse<object>.Ok(
            null,
            "Menu item deleted successfully."));
    }

    /// <summary>
    /// Maps MenuItem entity to MenuItemResponse.
    /// </summary>
    private static MenuItemResponse ToResponse(MenuItem menuItem)
    {
        return new MenuItemResponse
        {
            Id = menuItem.Id,
            CategoryId = menuItem.CategoryId,
            Name = menuItem.Name,
            Description = menuItem.Description,
            Price = menuItem.Price,
            DiscountPrice = menuItem.DiscountPrice,
            IsVeg = menuItem.IsVeg,
            IsAvailable = menuItem.IsAvailable,
            PreparationTimeInMinutes = menuItem.PreparationTimeInMinutes,
            ImageUrl = menuItem.ImageUrl
        };
    }
}