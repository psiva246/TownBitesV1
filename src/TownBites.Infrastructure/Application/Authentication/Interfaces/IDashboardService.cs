using TownBites.Shared.Contracts.Responses;

namespace TownBites.Infrastructure.Interfaces;

public interface IDashboardService
{
    Task<DashboardResponse> GetDashboardAsync(int restaurantId);
}