using Microsoft.AspNetCore.Mvc;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.API.Controllers;

[ApiController]
[Route("api")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Create a category for a restaurant.
    /// </summary>
    [HttpPost("restaurants/{restaurantId:int}/categories")]
    public async Task<IActionResult> Create(
        int restaurantId,
        [FromBody] CreateCategoryRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));
        }

        try
        {
            var category = await _categoryService.CreateAsync(restaurantId, request);

            return Ok(ApiResponse<CategoryResponse>.Ok(
                ToResponse(category),
                "Category created successfully."));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<object>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// Get all categories for a restaurant.
    /// </summary>
    [HttpGet("restaurants/{restaurantId:int}/categories")]
    public async Task<IActionResult> GetByRestaurant(int restaurantId)
    {
        var categories = await _categoryService.GetByRestaurantAsync(restaurantId);

        var response = categories
            .Select(ToResponse)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToList();

        return Ok(ApiResponse<IEnumerable<CategoryResponse>>.Ok(response));
    }

    /// <summary>
    /// Update a category.
    /// </summary>
    [HttpPut("categories/{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateCategoryRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse<object>.Fail("Validation failed."));
        }

        var category = await _categoryService.UpdateAsync(id, request);

        if (category == null)
        {
            return NotFound(ApiResponse<object>.Fail("Category not found."));
        }

        return Ok(ApiResponse<CategoryResponse>.Ok(
            ToResponse(category),
            "Category updated successfully."));
    }

    /// <summary>
    /// Deactivate a category.
    /// </summary>
    [HttpDelete("categories/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _categoryService.DeactivateAsync(id);

        if (!success)
        {
            return NotFound(ApiResponse<object>.Fail("Category not found."));
        }

        return Ok(ApiResponse<object>.Ok(
            null,
            "Category deactivated successfully."));
    }

    /// <summary>
    /// Maps Category entity to CategoryResponse.
    /// </summary>
    private static CategoryResponse ToResponse(Category category)
    {
        return new CategoryResponse
        {
            Id = category.Id,
            RestaurantId = category.RestaurantId,
            Name = category.Name,
            DisplayOrder = category.DisplayOrder,
            IsActive = category.IsActive
        };
    }
}