using Microsoft.Extensions.Options;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Orders;
using TownBites.AdminWeb.Services;

public class OrderApiService : BaseApiService, IOrderApiService
{
    public OrderApiService(HttpClient client, ITokenProvider tokenProvider,
        IOptions<ApiSettings> options) : base(client, tokenProvider)
    {
        client.BaseAddress = new Uri(options.Value.BaseUrl);
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
}