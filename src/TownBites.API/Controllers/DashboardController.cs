using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Responses;
using TownBites.API.Helpers;
namespace TownBites.API.Controllers;

[ApiController]
[Route("api/dashboard")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var restaurantClaim = User.FindFirst("RestaurantId")?.Value;

        if (string.IsNullOrWhiteSpace(restaurantClaim))
            return Unauthorized();

        var restaurantId = int.Parse(restaurantClaim);

        var dashboard = await _dashboardService.GetDashboardAsync(restaurantId);

        return Ok(new ApiResponse<DashboardResponse>
        {
            Success = true,
            Message = "Dashboard loaded successfully.",
            Data = dashboard
        });
    }
}