using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PlaceOrderResponse?> PlaceOrderAsync(PlaceOrderRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/orders", request);

        if (!response.IsSuccessStatusCode)
            return null;

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<PlaceOrderResponse>>();

        if (apiResponse == null || !apiResponse.Success)
            return null;

        return apiResponse.Data;
    }

    public async Task<ApiResponse<int>> CheckoutAsync(CheckoutRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/orders/checkout",
            request);

        return await response.Content
            .ReadFromJsonAsync<ApiResponse<int>>()
            ?? new ApiResponse<int>
            {
                Success = false,
                Message = "Unknown error"
            };
    }

    public async Task<List<OrderDto>> GetMyOrdersAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<OrderDto>>>("api/orders/my");

        return response?.Data ?? new();
    }

    public async Task<OrderDetailsDto?> GetOrderDetailsAsync(int orderId)
    {
        var response =
            await _httpClient.GetFromJsonAsync<ApiResponse<OrderDetailsDto>>
            ($"api/orders/{orderId}");

        return response?.Data;
    }
}