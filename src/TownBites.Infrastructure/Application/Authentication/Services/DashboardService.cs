using Microsoft.EntityFrameworkCore;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Responses;
using TownBites.Shared.Enums;

namespace TownBites.Infrastructure.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _dbContext;

    public DashboardService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DashboardResponse> GetDashboardAsync(int restaurantId)
    {
        var today = DateTime.Today;

        var orders = _dbContext.Orders
            .Where(x => x.RestaurantId == restaurantId);

        var todayOrders = await orders
            .Where(x => x.OrderedOn.Date == today)
            .ToListAsync();

        var recentOrders = await orders
            .OrderByDescending(x => x.OrderedOn)
            .Take(10)
            .Select(x => new OrderResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                RestaurantId = x.RestaurantId,
                OrderedOn = x.OrderedOn,
                Status = x.Status,
                TotalAmount = x.TotalAmount
            })
            .ToListAsync();

        var topSellingItems = await _dbContext.OrderItems
            .Where(x => x.Order.RestaurantId == restaurantId)
            .GroupBy(x => x.MenuItem.Name)
            .Select(g => new TopSellingItemResponse
            {
                Name = g.Key,
                QuantitySold = g.Sum(x => x.Quantity)
            })
            .OrderByDescending(x => x.QuantitySold)
            .Take(10)
            .ToListAsync();

        //var revenueChart = await orders
        //    .Where(x => x.OrderedOn >= DateTime.Today.AddDays(-6))
        //    .GroupBy(x => x.OrderedOn.Date)
        //    .Select(g => new RevenueChartResponse
        //    {
        //        Day = g.Key.ToString("ddd"),
        //        Revenue = g.Sum(x => x.TotalAmount)
        //    })
        //    .OrderBy(x => x.Day)
        //    .ToListAsync();

        var revenueData = await orders
            .Where(x => x.OrderedOn >= DateTime.Today.AddDays(-6))
            .GroupBy(x => x.OrderedOn.Date)
            .Select(g => new
            {
                Date = g.Key,
                Revenue = g.Sum(x => x.TotalAmount)
            })
            .OrderBy(x => x.Date)
            .ToListAsync();

        var revenueChart = revenueData
            .Select(x => new RevenueChartResponse
            {
                Day = x.Date.ToString("ddd"),
                Revenue = x.Revenue
            })
            .ToList();

        var statusChart = await orders
            .GroupBy(x => x.Status)
            .Select(g => new OrderStatusChartResponse
            {
                Status = g.Key.ToString(),
                Count = g.Count()
            })
            .ToListAsync();

        return new DashboardResponse
        {
            TodayOrders = todayOrders.Count,
            PendingOrders = await orders.CountAsync(x => x.Status == OrderStatus.Pending),
            CompletedOrders = await orders.CountAsync(x => x.Status == OrderStatus.Delivered),
            TodayRevenue = todayOrders.Sum(x => x.TotalAmount),
            RecentOrders = recentOrders,
            TopSellingItems = topSellingItems,
            RevenueChart = revenueChart,
            StatusChart = statusChart
        };
    }
}