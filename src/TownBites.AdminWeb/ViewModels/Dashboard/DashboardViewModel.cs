using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using TownBites.AdminWeb.Interfaces;
using TownBites.Shared.Contracts.Responses;

namespace TownBites.AdminWeb.ViewModels.Dashboard;

public partial class DashboardViewModel
{
    private readonly IDashboardApiService _dashboardService;

    //[ObservableProperty]
    private DashboardResponse? dashboard;

    public DashboardViewModel(IDashboardApiService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public int TodayOrders { get; set; }
    public int PendingOrders { get; set; }
    public int CompletedOrders { get; set; }
    public decimal TodayRevenue { get; set; }
    public List<OrderResponse> RecentOrders { get; set; } = new();
    public List<TopSellingItemViewModel> TopSellingItems { get; set; } = new();
    public List<RevenueChartResponse> RevenueChart { get; set; } = new();
    public List<OrderStatusChartResponse> StatusChart { get; set; } = new();

    [RelayCommand]
    private async Task LoadDashboard()
    {
        dashboard = await _dashboardService.GetDashboardAsync();
    }
}