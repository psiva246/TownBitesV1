using System.Threading.Tasks;
using System.Collections.Generic;
using TownBites.CustomerApp.Models;


namespace TownBites.CustomerApp.Interfaces;

public interface IOrderService
{
    //Task<List<OrderSummaryDto>> GetMyOrdersAsync();

    //Task<OrderDetailsDto?> GetOrderAsync(int orderId);

    Task<PlaceOrderResponse?> PlaceOrderAsync(PlaceOrderRequest request);
    Task<ApiResponse<int>> CheckoutAsync(CheckoutRequest request);

    Task<List<OrderDto>> GetMyOrdersAsync();
    //Task<OrderDto?> GetOrderAsync(int id);
    Task<OrderDetailsDto?> GetOrderDetailsAsync(int orderId);
}