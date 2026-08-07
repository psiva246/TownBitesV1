using System.Net.Http.Json;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.Services;

public class RestaurantService : IRestaurantService
{
    private readonly HttpClient _httpClient;

    public RestaurantService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RestaurantDto>> GetRestaurantsAsync()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ApiResponse<List<RestaurantDto>>>("api/restaurants");

        if (response == null)
            return new List<RestaurantDto>();

        if (!response.Success)
            throw new Exception(response.Message);

        return response.Data ?? new List<RestaurantDto>();
    }
}