using TownBites.AdminWeb.Models.Auth;
using TownBites.AdminWeb.Models.Dashboard;
using TownBites.AdminWeb.ViewModels;
using TownBites.AdminWeb.ViewModels.Dashboard;

namespace TownBites.AdminWeb.Interfaces;

public interface IDashboardApiService
{
    //Task<DashboardDto> GetDashboardAsync();
    Task<DashboardViewModel> GetDashboardAsync();
}