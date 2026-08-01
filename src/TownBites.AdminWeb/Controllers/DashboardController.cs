using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models.Dashboard;

namespace TownBites.AdminWeb.Controllers;

//[Authorize]
public class DashboardController : Controller
{
    private readonly IDashboardApiService _dashboardApiService;

    public DashboardController(IDashboardApiService dashboardApiService)
    {
        _dashboardApiService = dashboardApiService;
    }

    public async Task<IActionResult> Index()
    {
        var dashboard = await _dashboardApiService.GetDashboardAsync();

        return View(dashboard);
    }
}