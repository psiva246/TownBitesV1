using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TownBites.Application.DTOs;
using TownBites.Application.Interfaces;
using TownBites.Application.Requests;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;

namespace TownBites.Application.Services;

public class CategoryService : ICategoryService
{
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
                Name = x.Name
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
                Name = category.Name,
                Description = category.Description,
                RestaurantId = category.RestaurantId,
                IsActive = category.IsActive,
                DisplayOrder = category.DisplayOrder
            }
        };
    }

    public async Task<ApiResponse<CategoryDto>> CreateAsync(CreateCategoryRequest request)
    {
        var exists = await _context.Categories
            .AnyAsync(x => x.Name == request.Name && x.RestaurantId == request.RestaurantId);

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
            Name = request.Name,
            RestaurantId = request.RestaurantId,
            DisplayOrder = request.DisplayOrder,
            IsActive = request.IsActive,
            Description = request.Description
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

    public async Task<ApiResponse<bool>> UpdateAsync(
        int id,
        UpdateCategoryRequest request)
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

        category.Name = request.Name;
        category.Description = request.Description;
        category.IsActive = request.IsActive;
        category.DisplayOrder = request.DisplayOrder;

        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Category updated successfully.",
            Data = true
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
        category.IsActive = false;
        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Category deleted successfully.",
            Data = true
        };
    }
}