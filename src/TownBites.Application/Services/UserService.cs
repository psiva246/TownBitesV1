using Microsoft.EntityFrameworkCore;
using TownBites.Application.DTOs;
using TownBites.Application.Interfaces;
using TownBites.Application.Requests;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;

namespace TownBites.Application.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllAsync()
    {
        var users = await _context.Users
            .OrderBy(x => x.Name)
            .Select(x => new UserDto
            {
                Id = x.Id,
                FullName = x.Name,
                Email = x.Email,
                Role = x.Role.ToString()
            })
            .ToListAsync();

        return new ApiResponse<IEnumerable<UserDto>>
        {
            Success = true,
            Message = "Users retrieved successfully.",
            Data = users
        };
    }

    public async Task<ApiResponse<UserDto>> GetByIdAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "User not found."
            };
        }

        return new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User retrieved successfully.",
            Data = new UserDto
            {
                Id = user.Id,
                FullName = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            }
        };
    }

    public async Task<ApiResponse<UserDto>> CreateAsync(CreateUserRequest request)
    {
        var exists = await _context.Users
            .AnyAsync(x => x.Email == request.Email);

        if (exists)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "Email already exists."
            };
        }

        var user = new User
        {
            Name = request.FullName,
            Email = request.Email,
            PasswordHash = request.Password,
            Role = request.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User created successfully.",
            Data = new UserDto
            {
                Id = user.Id,
                FullName = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            }
        };
    }

    public async Task<ApiResponse<UserDto>> UpdateAsync(
        int id,
        UpdateUserRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return new ApiResponse<UserDto>
            {
                Success = false,
                Message = "User not found."
            };
        }

        user.Name = request.FullName;
        user.Email = request.Email;
        user.Role = request.Role;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return new ApiResponse<UserDto>
        {
            Success = true,
            Message = "User updated successfully.",
            Data = new UserDto
            {
                Id = user.Id,
                FullName = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            }
        };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "User not found.",
                Data = false
            };
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "User deleted successfully.",
            Data = true
        };
    }
}