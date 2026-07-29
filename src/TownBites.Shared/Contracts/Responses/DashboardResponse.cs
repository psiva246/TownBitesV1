namespace TownBites.Shared.Contracts.Responses;

public class DashboardResponse
{
    public int TodayOrders { get; set; }

    public int PendingOrders { get; set; }

    public int PreparingOrders { get; set; }

    public int ReadyOrders { get; set; }

    public int DeliveredOrders { get; set; }

    public int CancelledOrders { get; set; }

    public decimal TodayRevenue { get; set; }

    public List<PopularMenuItemResponse> PopularItems { get; set; } = new();
}

public class PopularMenuItemResponse
{
    public int MenuItemId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int TotalOrdered { get; set; }
}