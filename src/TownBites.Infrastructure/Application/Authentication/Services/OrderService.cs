using Azure.Core;
using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;
using TownBites.Shared.Enums;
using TownBites.Shared.Helpers;
using AutoMapper;

namespace TownBites.Infrastructure.Application.Authentication.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        public OrderService(ApplicationDbContext dbContext, /*IMapper mapper,*/ INotificationService notificationService)
        {
            _dbContext = dbContext;
            //_mapper = mapper;
            _notificationService = notificationService;
        }
        public async Task<List<OrderResponse>> GetPendingOrdersAsync()
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(x => x.Items)
                .Where(x => x.Status == OrderStatus.Pending)
                .OrderBy(x => x.OrderedOn)
                .Select(x => new OrderResponse
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    TotalAmount = x.TotalAmount,
                    Status = x.Status,
                    OrderedOn = x.OrderedOn,

                    Items = x.Items.Select(i => new OrderItemResponse
                    {
                        MenuItemId = i.MenuItemId,
                        ItemName = i.ItemName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        TotalPrice = i.TotalPrice
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<OrderResponse?> GetOrderAsync(int orderId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(x => x.Items)
                .Where(x => x.Id == orderId)
                .Select(x => new OrderResponse
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    TotalAmount = x.TotalAmount,
                    Status = x.Status,
                    OrderedOn = x.OrderedOn,

                    Items = x.Items.Select(i => new OrderItemResponse
                    {
                        MenuItemId = i.MenuItemId,
                        ItemName = i.ItemName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        TotalPrice = i.TotalPrice
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
                return false;

            if (!OrderStatusTransitions.Allowed.TryGetValue(order.Status, out var allowed) || !allowed.Contains(status))
            {
                throw new InvalidOperationException($"Cannot change order from {order.Status} to {status}.");
            }

            order.Status = status;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<OrderResponse>> GetCustomerOrdersAsync(int userId)
        {
            try
            {
                try
                {
                    var list1 = await _dbContext.Orders.ToListAsync();
                    var list = await _dbContext.Orders
                        //.AsNoTracking()
                        //.Include(o => o.Items)
                        .Where(o => o.UserId == userId)
                        //.OrderByDescending(o => o.OrderedOn)
                        .ToListAsync();
                }
                catch (Exception ex)
                {
                }

                return await _dbContext.Orders
                    .AsNoTracking()
                    .Include(o => o.Items)
                    .Where(o => o.UserId == userId)
                    .OrderByDescending(o => o.OrderedOn)
                    .Select(o => new OrderResponse
                    {
                        Id = o.Id,
                        UserId = o.UserId,
                        RestaurantId = o.RestaurantId,
                        TotalAmount = o.TotalAmount,
                        Status = o.Status,
                        OrderedOn = o.OrderedOn,

                        Items = o.Items.Select(i => new OrderItemResponse
                        {
                            MenuItemId = i.MenuItemId,
                            ItemName = i.ItemName,
                            Quantity = i.Quantity,
                            UnitPrice = i.UnitPrice,
                            TotalPrice = i.TotalPrice
                        }).ToList()
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching customer orders: {ex.Message}");
                return new List<OrderResponse>();
            }
        }

        public async Task<OrderResponse?> GetCustomerOrderAsync(int userId, int orderId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Where(o => o.Id == orderId &&
                            o.UserId == userId)
                .Select(o => new OrderResponse
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    RestaurantId = o.RestaurantId,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderedOn = o.OrderedOn,

                    Items = o.Items.Select(i => new OrderItemResponse
                    {
                        MenuItemId = i.MenuItemId,
                        ItemName = i.ItemName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        TotalPrice = i.TotalPrice
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
