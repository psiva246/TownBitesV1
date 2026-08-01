using TownBites.Shared.Contracts.Responses;

namespace TownBites.AdminWeb.ViewModels.Dashboard;

public class DashboardViewModel
{
    public int TodayOrders { get; set; }
    public int PendingOrders { get; set; }
    public int CompletedOrders { get; set; }
    public decimal TodayRevenue { get; set; }
    public List<OrderResponse> RecentOrders { get; set; } = new();
    public List<TopSellingItemViewModel> TopSellingItems { get; set; } = new();
    public List<RevenueChartResponse> RevenueChart { get; set; } = new();
    public List<OrderStatusChartResponse> StatusChart { get; set; } = new();
}