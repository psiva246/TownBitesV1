using TownBites.AdminWeb.Models.Dashboard;
namespace TownBites.AdminWeb.Models.Dashboard
{
    public class DashboardDto
    {
        public DashboardCardDto Cards { get; set; } = new();

        public List<RevenueChartDto> Revenue { get; set; } = new();

        public List<OrderStatusDto> Status { get; set; } = new();

        public List<RecentOrderDto> RecentOrders { get; set; } = new();

        public List<TopSellingItemDto> TopSellingItems { get; set; } = new();
    }
}