namespace TownBites.Application.DTOs;

public class DashboardResponse
{
    public int TotalOrders { get; set; }

    public int TodayOrders { get; set; }

    public decimal TodayRevenue { get; set; }

    public decimal TotalRevenue { get; set; }

    public int PendingOrders { get; set; }

    public int PreparingOrders { get; set; }

    public int ReadyOrders { get; set; }

    public int DeliveredOrders { get; set; }

    public int CancelledOrders { get; set; }
}