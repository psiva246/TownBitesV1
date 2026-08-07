using Microsoft.Extensions.Options;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Common;
using TownBites.AdminWeb.Models.Customer;
public class CustomerApiService : ICustomerApiService
{
    private readonly HttpClient _http;

    public CustomerApiService(HttpClient http, IOptions<ApiSettings> options)
    {
        _http = http;
        _http.BaseAddress = new Uri(options.Value.BaseUrl);
    }

    public async Task<List<CustomerDto>> GetCustomersAsync()
    {
        var result = await _http.GetFromJsonAsync<ApiResponse<List<CustomerDto>>>("api/customers");

        return result?.Data ?? new();
    }

    public async Task<CustomerDetailsDto?> GetCustomerAsync(int id)
    {
        var response = await _http.GetFromJsonAsync<ApiResponse<CustomerDetailsDto>>($"api/customers/{id}");

        return response?.Data;
    }
}