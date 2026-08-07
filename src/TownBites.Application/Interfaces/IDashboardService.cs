using TownBites.Application.DTOs;

namespace TownBites.Application.Interfaces;

public interface IDashboardService
{
    Task<ApiResponse<DashboardResponse>> GetDashboardAsync(int restaurantId);
}