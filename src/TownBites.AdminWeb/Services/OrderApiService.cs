using Microsoft.Extensions.Options;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Orders;
using TownBites.AdminWeb.Services;
using TownBites.Shared.Contracts.Responses;
using TownBites.AdminWeb.Models.Common;
public class OrderApiService : BaseApiService, IOrderApiService
{
    private readonly HttpClient _httpClient;

    public OrderApiService(HttpClient client, ITokenProvider tokenProvider, IOptions<ApiSettings> options) : base(client, tokenProvider)
    {
        client.BaseAddress = new Uri(options.Value.BaseUrl);
        _httpClient = client;
    }

    public Task<List<OrderListDto>> GetAllAsync()
    {
        return GetAsync<List<OrderListDto>>("api/orders");
    }

    public Task<OrderDetailsDto?> GetByIdAsync(int id)
    {
        return GetAsync<OrderDetailsDto>($"api/orders/{id}");
    }

    public Task UpdateStatusAsync(UpdateOrderStatusRequest request)
    {
        return PutAsync($"api/orders/{request.OrderId}/status", request);
    }

    public async Task<ApiResponse<List<OrderResponse>>> GetPendingOrdersAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<OrderResponse>>>("api/orders/pending");

            return response ?? new ApiResponse<List<OrderResponse>>
            {
                Success = false,
                Message = "No response received from server.",
                Data = new List<OrderResponse>()
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<OrderResponse>>
            {
                Success = false,
                Message = ex.Message,
                Data = new List<OrderResponse>()
            };
        }
    }
}