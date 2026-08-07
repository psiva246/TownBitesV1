using Microsoft.EntityFrameworkCore;
using TownBites.Application.DTOs;
using TownBites.Application.Interfaces;
using TownBites.Infrastructure.Data;
using TownBites.Shared.Enums;
namespace TownBites.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<DashboardResponse>> GetDashboardAsync(int restaurantId)
    {
        var today = DateTime.Today;

        var orders = _context.Orders
            .Where(x => x.RestaurantId == restaurantId);

        var response = new DashboardResponse
        {
            TotalOrders = await orders.CountAsync(),

            TodayOrders = await orders
                .CountAsync(x => x.OrderDate.Date == today),

            TotalRevenue = await orders
                .Where(x => x.Status == OrderStatus.Delivered)
                .SumAsync(x => (decimal?)x.TotalAmount) ?? 0,

            TodayRevenue = await orders
                .Where(x => x.OrderDate.Date == today &&
                            x.Status == OrderStatus.Delivered)
                .SumAsync(x => (decimal?)x.TotalAmount) ?? 0,

            PendingOrders = await orders
                .CountAsync(x => x.Status == OrderStatus.Pending),

            PreparingOrders = await orders
                .CountAsync(x => x.Status == OrderStatus.Preparing),

            ReadyOrders = await orders
                .CountAsync(x => x.Status == OrderStatus.Ready),

            DeliveredOrders = await orders
                .CountAsync(x => x.Status == OrderStatus.Delivered),

            CancelledOrders = await orders
                .CountAsync(x => x.Status == OrderStatus.Cancelled)
        };

        return new ApiResponse<DashboardResponse>
        {
            Success = true,
            Message = "Dashboard loaded successfully.",
            Data = response
        };
    }
}