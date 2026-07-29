using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Restaurant")]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    // Temporary implementation
    // Replace with logged-in restaurant ID later
    private int GetRestaurantId()
    {
        var restaurantId = User.FindFirst("RestaurantId")?.Value;

        if (string.IsNullOrEmpty(restaurantId))
            throw new UnauthorizedAccessException("Restaurant not authenticated.");

        return int.Parse(restaurantId);
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboard()
    {
        var dashboard = await _dashboardService.GetDashboardAsync(GetRestaurantId());

        return Ok(ApiResponse<DashboardResponse>.Ok(dashboard, "Dashboard loaded successfully."));
    }
}