using TownBites.Shared.Contracts.Responses;
using TownBites.Shared.Enums;

namespace TownBites.Infrastructure.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponse>> GetPendingOrdersAsync();

    Task<OrderResponse?> GetOrderAsync(int orderId);

    Task<bool> UpdateStatusAsync(int orderId, OrderStatus status);

    Task<List<OrderResponse>> GetCustomerOrdersAsync(int userId);

    Task<OrderResponse?> GetCustomerOrderAsync(int userId, int orderId);
}