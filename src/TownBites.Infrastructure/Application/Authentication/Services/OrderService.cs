using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TownBites.Application.Common;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Common;
using TownBites.Shared.Contracts.Requests;
using TownBites.Shared.Contracts.Responses;
using TownBites.Shared.Enums;

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
                .OrderBy(x => x.CreatedOn)
                .Select(x => new OrderResponse
                {
                    Id = x.Id,
                    UserId = x.CustomerId,
                    TotalAmount = x.TotalAmount,
                    Status = x.Status,
                    OrderedOn = x.CreatedOn,

                    Items = x.Items.Select(i => new OrderItemResponse
                    {
                        MenuItemId = i.MenuItemId,
                        ItemName = i.MenuItemName,
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
                    UserId = x.CustomerId,
                    TotalAmount = x.TotalAmount,
                    Status = x.Status,
                    OrderedOn = x.CreatedOn,

                    Items = x.Items.Select(i => new OrderItemResponse
                    {
                        MenuItemId = i.MenuItemId,
                        ItemName = i.MenuItemName,
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

            //if (!OrderStatusTransitions.GetAllowedTransitions.pending.TryGetValue(order.Status, out var allowed) || !allowed.Contains(status))
            //{
            //    throw new InvalidOperationException($"Cannot change order from {order.Status} to {status}.");
            //}

            order.Status = status;
            //await _hubContext.Clients.Group($"order-{order.Id}").SendAsync("OrderStatusChanged", order.Status);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<ApiResponse<bool>> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);

            if (order == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Order not found"
                };
            }

            if (!OrderStatusTransitions.CanTransition(order.Status, newStatus))
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = $"Cannot change order status from {order.Status} to {newStatus}."
                };
            }

            order.Status = newStatus;
            order.UpdatedOn = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Order status updated successfully.",
                Data = true
            };
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
                        .Where(o => o.CustomerId == userId)
                        //.OrderByDescending(o => o.OrderedOn)
                        .ToListAsync();
                }
                catch (Exception ex)
                {
                }

                return await _dbContext.Orders
                    .AsNoTracking()
                    .Include(o => o.Items)
                    .Where(o => o.CustomerId == userId)
                    .OrderByDescending(o => o.CreatedOn)
                    .Select(o => new OrderResponse
                    {
                        Id = o.Id,
                        UserId = o.CustomerId,
                        RestaurantId = o.RestaurantId,
                        TotalAmount = o.TotalAmount,
                        Status = o.Status,
                        OrderedOn = o.CreatedOn,

                        Items = o.Items.Select(i => new OrderItemResponse
                        {
                            MenuItemId = i.MenuItemId,
                            ItemName = i.MenuItemName,
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
                            o.CustomerId == userId)
                .Select(o => new OrderResponse
                {
                    Id = o.Id,
                    UserId = o.CustomerId,
                    RestaurantId = o.RestaurantId,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderedOn = o.CreatedOn,

                    Items = o.Items.Select(i => new OrderItemResponse
                    {
                        MenuItemId = i.MenuItemId,
                        ItemName = i.MenuItemName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        TotalPrice = i.TotalPrice
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
