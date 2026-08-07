using TownBites.Application.DTOs;
using TownBites.Application.Requests;

namespace TownBites.Application.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequest request);
}