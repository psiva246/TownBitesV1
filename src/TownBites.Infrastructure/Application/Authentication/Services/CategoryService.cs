using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Models;

namespace TownBites.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    //private readonly ApplicationDbContext _dbContext;

    //public CategoryService(ApplicationDbContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    //public async Task<Category> CreateAsync(
    //    int restaurantId,
    //    CreateCategoryRequest request)
    //{
    //    var restaurant = await _dbContext.Restaurants
    //        .FirstOrDefaultAsync(x => x.Id == restaurantId && x.IsActive);

    //    if (restaurant == null)
    //        throw new Exception("Restaurant not found.");

    //    var category = new Category
    //    {
    //        RestaurantId = restaurantId,
    //        Name = request.Name,
    //        DisplayOrder = request.DisplayOrder,
    //        IsActive = true
    //    };

    //    _dbContext.Categories.Add(category);

    //    await _dbContext.SaveChangesAsync();

    //    return category;
    //}

    //public async Task<List<Category>> GetByRestaurantAsync(int restaurantId)
    //{
    //    return await _dbContext.Categories
    //        .Where(x => x.RestaurantId == restaurantId && x.IsActive)
    //        .OrderBy(x => x.DisplayOrder)
    //        .ThenBy(x => x.Name)
    //        .ToListAsync();
    //}

    //public async Task<Category?> UpdateAsync(
    //    int id,
    //    UpdateCategoryRequest request)
    //{
    //    var category = await _dbContext.Categories
    //        .FirstOrDefaultAsync(x => x.Id == id);

    //    if (category == null)
    //        return null;

    //    category.Name = request.Name;
    //    category.DisplayOrder = request.DisplayOrder;
    //    category.IsActive = request.IsActive;

    //    await _dbContext.SaveChangesAsync();

    //    return category;
    //}

    //public async Task<bool> DeactivateAsync(int id)
    //{
    //    var category = await _dbContext.Categories
    //        .FirstOrDefaultAsync(x => x.Id == id);

    //    if (category == null)
    //        return false;

    //    category.IsActive = false;

    //    await _dbContext.SaveChangesAsync();

    //    return true;
    //}

    private readonly ApplicationDbContext _context;
    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IEnumerable<CategoryDto>>> GetAllAsync(int restaurantId)
    {
        var categories = await _context.Categories
            .Where(x => x.RestaurantId == restaurantId)
            .OrderBy(x => x.Name)
            .Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DisplayOrder = x.DisplayOrder,
                RestaurantId = x.RestaurantId,
                IsActive = x.IsActive                
            })
            .ToListAsync();

        return new ApiResponse<IEnumerable<CategoryDto>>
        {
            Success = true,
            Message = "Categories retrieved successfully.",
            Data = categories
        };
    }

    public async Task<ApiResponse<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return new ApiResponse<CategoryDto>
            {
                Success = false,
                Message = "Category not found."
            };
        }

        return new ApiResponse<CategoryDto>
        {
            Success = true,
            Message = "Category retrieved successfully.",
            Data = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            }
        };
    }

    public async Task<ApiResponse<CategoryDto>> CreateAsync(CreateCategoryRequest request)
    {
        var exists = await _context.Categories
            .AnyAsync(x => x.Name == request.Name);

        if (exists)
        {
            return new ApiResponse<CategoryDto>
            {
                Success = false,
                Message = "Category already exists."
            };
        }

        var category = new Category
        {
            Name = request.Name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return new ApiResponse<CategoryDto>
        {
            Success = true,
            Message = "Category created successfully.",
            Data = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            }
        };
    }

    public async Task<ApiResponse<CategoryDto>> UpdateAsync(
        int id,
        UpdateCategoryRequest request)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return new ApiResponse<CategoryDto>
            {
                Success = false,
                Message = "Category not found."
            };
        }

        category.Name = request.Name;

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return new ApiResponse<CategoryDto>
        {
            Success = true,
            Message = "Category updated successfully.",
            Data = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            }
        };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Category not found.",
                Data = false
            };
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Category deleted successfully.",
            Data = true
        };
    }
}