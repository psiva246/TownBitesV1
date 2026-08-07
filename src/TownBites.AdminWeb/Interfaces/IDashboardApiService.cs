using TownBites.AdminWeb.Models.Auth;
using TownBites.AdminWeb.Models.Dashboard;
using TownBites.AdminWeb.ViewModels;
using TownBites.AdminWeb.ViewModels.Dashboard;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.AdminWeb.Interfaces;

public interface IDashboardApiService
{
    //Task<DashboardViewModel> GetDashboardAsync();
    Task<DashboardResponse?> GetDashboardAsync();
}