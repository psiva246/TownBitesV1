using TownBites.Application.DTOs;
using TownBites.Application.Requests;

namespace TownBites.Application.Interfaces;

public interface IUserService
{
    Task<ApiResponse<IEnumerable<UserDto>>> GetAllAsync();

    Task<ApiResponse<UserDto>> GetByIdAsync(int id);

    Task<ApiResponse<UserDto>> CreateAsync(CreateUserRequest request);

    Task<ApiResponse<UserDto>> UpdateAsync(
        int id,
        UpdateUserRequest request);

    Task<ApiResponse<bool>> DeleteAsync(int id);
}