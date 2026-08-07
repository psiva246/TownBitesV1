using Microsoft.EntityFrameworkCore;
using TownBites.Application.DTOs;
using TownBites.Application.Interfaces;
using TownBites.Application.Requests;
using TownBites.Domain.Entities;
using TownBites.Infrastructure.Data;

namespace TownBites.Application.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<IEnumerable<OrderDto>>> GetAllAsync()
    {
        var orders = await _context.Orders
            .OrderByDescending(x => x.OrderDate)
            .Select(x => new OrderDto
            {
                Id = x.Id,
                RestaurantId = x.RestaurantId,
                //CustomerName = x.CustomerName,
                //CustomerPhone = x.CustomerPhone,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                OrderDate = x.OrderDate
            })
            .ToListAsync();

        return new ApiResponse<IEnumerable<OrderDto>>
        {
            Success = true,
            Message = "Orders retrieved successfully.",
            Data = orders
        };
    }

    public async Task<ApiResponse<OrderDto>> GetByIdAsync(int id)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order == null)
        {
            return new ApiResponse<OrderDto>
            {
                Success = false,
                Message = "Order not found."
            };
        }

        var dto = new OrderDto
        {
            Id = order.Id,
            RestaurantId = order.RestaurantId,
            //CustomerName = order.CustomerName,
            //CustomerPhone = order.CustomerPhone,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            OrderDate = order.OrderDate
        };

        return new ApiResponse<OrderDto>
        {
            Success = true,
            Message = "Order retrieved successfully.",
            Data = dto
        };
    }

    public async Task<ApiResponse<IEnumerable<OrderDto>>> GetByRestaurantAsync(int restaurantId)
    {
        var orders = await _context.Orders
            .Where(x => x.RestaurantId == restaurantId)
            .OrderByDescending(x => x.OrderDate)
            .Select(x => new OrderDto
            {
                Id = x.Id,
                RestaurantId = x.RestaurantId,
                //CustomerName = x.CustomerName,
                //CustomerPhone = x.CustomerPhone,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                OrderDate = x.OrderDate
            })
            .ToListAsync();

        return new ApiResponse<IEnumerable<OrderDto>>
        {
            Success = true,
            Message = "Orders retrieved successfully.",
            Data = orders
        };
    }

    public async Task<ApiResponse<OrderDto>> CreateAsync(CreateOrderRequest request)
    {
        var order = new Order
        {
            RestaurantId = request.RestaurantId,
            //CustomerName = request.CustomerName,
            //CustomerPhone = request.CustomerPhone,
            TotalAmount = request.TotalAmount,
            Status = Shared.Enums.OrderStatus.Pending,
            OrderDate = DateTime.Now
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var dto = new OrderDto
        {
            Id = order.Id,
            RestaurantId = order.RestaurantId,
            //CustomerName = order.CustomerName,
            //CustomerPhone = order.CustomerPhone,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            OrderDate = order.OrderDate
        };

        return new ApiResponse<OrderDto>
        {
            Success = true,
            Message = "Order created successfully.",
            Data = dto
        };
    }

    public async Task<ApiResponse<bool>> UpdateStatusAsync(UpdateOrderStatusRequest request)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == request.OrderId);

        if (order == null)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Order not found.",
                Data = false
            };
        }

        order.Status = request.Status;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Order status updated successfully.",
            Data = true
        };
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order == null)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                Message = "Order not found.",
                Data = false
            };
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return new ApiResponse<bool>
        {
            Success = true,
            Message = "Order deleted successfully.",
            Data = true
        };
    }
}