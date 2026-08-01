namespace TownBites.Shared.Contracts.Responses;

public class DashboardResponse
{    public int TodayOrders { get; set; }
    public int PendingOrders { get; set; }
    public int CompletedOrders { get; set; }
    public decimal TodayRevenue { get; set; }
    public List<OrderResponse> RecentOrders { get; set; } = new();
    public List<TopSellingItemResponse> TopSellingItems { get; set; } = new();
    public List<RevenueChartResponse> RevenueChart { get; set; } = new();
    public List<OrderStatusChartResponse> StatusChart { get; set; } = new();
}
//public class DashboardResponse
//{
//    public int TodayOrders { get; set; }
//    public int PendingOrders { get; set; }
//    public int PreparingOrders { get; set; }
//    public int ReadyOrders { get; set; }
//    public int DeliveredOrders { get; set; }
//    public int CancelledOrders { get; set; }
//    public decimal TodayRevenue { get; set; }
//    public List<PopularMenuItemResponse> PopularItems { get; set; } = new();
//}

public class PopularMenuItemResponse
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TotalOrdered { get; set; }
}

public class TopSellingItemResponse
{
    public string Name { get; set; } = string.Empty;
    public int QuantitySold { get; set; }
}

public class RevenueChartResponse
{
    public string Day { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
}

public class OrderStatusChartResponse
{
    public string Status { get; set; } = string.Empty;
    public int Count { get; set; }
}