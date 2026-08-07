using Microsoft.EntityFrameworkCore;
using TownBites.Application.DTOs;
using TownBites.Application.Interfaces;
using TownBites.Application.Requests;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;

namespace TownBites.Application.Services;

public class RestaurantService : IRestaurantService
{
    private readonly ApplicationDbContext _context;

    public RestaurantService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IEnumerable<RestaurantDto>>> GetAllAsync()
    {
        var restaurants = await _context.Restaurants
            .OrderBy(x => x.Name)
            .Select(x => new RestaurantDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Phone = x.PhoneNumber,
                IsActive = x.IsActive
            })
            .ToListAsync();

        return new ApiResponse<IEnumerable<RestaurantDto>>
        {
            Success = true,
            Message = "Restaurants retrieved successfully.",
            Data = restaurants
        };
    }

    public async Task<ApiResponse<RestaurantDto>> GetByIdAsync(int id)
    {
        var restaurant = await _context.Restaurants
            .FirstOrDefaultAsync(x => x.Id == id);

        if (restaurant == null)
        {
            return new ApiResponse<RestaurantDto>
            {
                Success = false,
                Message = "Restaurant not found."
            };
        }

        return new ApiResponse<RestaurantDto>
        {
            Success = true,
            Message = "Restaurant retrieved successfully.",
            Data = new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Address = restaurant.Address,
                Phone = restaurant.PhoneNumber,
                IsActive = restaurant.IsActive
            }
        };
    }

    public async Task<ApiResponse<RestaurantDto>> CreateAsync(CreateRestaurantRequest request)
    {
        var exists = await _context.Restaurants
            .AnyAsync(x => x.Name == request.Name);

        if (exists)
        {
            return new ApiResponse<RestaurantDto>
            {
                Success = false,
                Message = "Restaurant already exists."
            };
        }

        var restaurant = new Restaurant
        {
            Name = request.Name,
            Address = request.Address,
            PhoneNumber = request.Phone,
            IsActive = true
        };

        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();

        return new ApiResponse<RestaurantDto>
        {
            Success = true,
            Message = "Restaurant created successfully.",
            Data = new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Address = restaurant.Address,
                Phone = restaurant.PhoneNumber,
                IsActive = restaurant.IsActive
            }
        };
    }

    public async Task<ApiResponse<RestaurantDto>> UpdateAsync(
        int id,
        UpdateRestaurantRequest request)
    {
        var restaurant = await _context.Restaurants
            .FirstOrDefaultAsync(x => x.Id == id);

        if (restaurant == null)
        {
            return new ApiResponse<RestaurantDto>
            {
                Success = false,
                Message = "Restaurant not found."
            };
        }

        restaurant.Name = request.Name;
        restaurant.Address = request.Address;
        restaurant.PhoneNumber = request.Phone;
        restaurant.IsActive = request.IsActive;

        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();

        return new ApiResponse<RestaurantDto>
        {
            Success = true,
            Message = "Restaurant updated successfully.",
            Data = new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Address = restaurant.Address,
                Phone = restaurant.PhoneNumber,
                IsActive = restaurant.IsActive
            }
        };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var restaurant = await _context.Restaurants
            .FirstOrDefaultAsync(x => x.Id == id);

        if (restaurant == null)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Restaurant not found.",
                Data = false
            };
        }

        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Restaurant deleted successfully.",
            Data = true
        };
    }
}