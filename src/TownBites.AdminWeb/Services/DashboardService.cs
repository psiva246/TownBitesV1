using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models.Common;
using TownBites.Shared.Contracts.Responses;

public class DashboardService : IDashboardApiService
{
    private readonly HttpClient _httpClient;

    public DashboardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DashboardResponse?> GetDashboardAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<DashboardResponse>>("api/dashboard");

        return response?.Data;
    }
}