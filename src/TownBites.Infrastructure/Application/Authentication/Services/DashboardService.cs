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

        var orders = await _dbContext.Orders
            .Include(o => o.Items)
            .Where(o => o.RestaurantId == restaurantId && o.OrderedOn.Date == today)
            .ToListAsync();

        var response = new DashboardResponse
        {
            TodayOrders = orders.Count,

            PendingOrders = orders.Count(x => x.Status == OrderStatus.Pending),

            PreparingOrders = orders.Count(x => x.Status == OrderStatus.Preparing),

            ReadyOrders = orders.Count(x => x.Status == OrderStatus.Ready),

            DeliveredOrders = orders.Count(x => x.Status == OrderStatus.Delivered),

            CancelledOrders = orders.Count(x => x.Status == OrderStatus.Cancelled),

            TodayRevenue = orders.Where(x => x.Status != OrderStatus.Cancelled).Sum(x => x.TotalAmount)
        };

        response.PopularItems = orders
            .SelectMany(x => x.Items)
            .GroupBy(x => new
            {
                x.MenuItemId,
                x.ItemName
            })
            .Select(g => new PopularMenuItemResponse
            {
                MenuItemId = g.Key.MenuItemId,
                Name = g.Key.ItemName,
                TotalOrdered = g.Sum(x => x.Quantity)
            })
            .OrderByDescending(x => x.TotalOrdered)
            .Take(10)
            .ToList();

        return response;
    }
}