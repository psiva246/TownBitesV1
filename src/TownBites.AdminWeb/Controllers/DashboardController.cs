using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models.Dashboard;
using TownBites.AdminWeb.Services;
using TownBites.AdminWeb.ViewModels.Dashboard;

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

        DashboardViewModel dashboardView = new DashboardViewModel(_dashboardApiService) 
                        { CompletedOrders = dashboard.CompletedOrders, PendingOrders = dashboard.PendingOrders //, TotalRevenue = dashboard.TotalRevenue
            , RecentOrders = dashboard.RecentOrders, TodayOrders = dashboard.TodayOrders, TodayRevenue = dashboard.TodayRevenue
            //, TopSellingItems = dashboard.TopSellingItems
            , StatusChart = dashboard.StatusChart, RevenueChart = dashboard.RevenueChart,  };

        return View(dashboardView);
    }
}