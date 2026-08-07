using System.Net.Http.Json;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.Services;

public class MenuService : IMenuService
{
    private readonly HttpClient _httpClient;

    public MenuService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MenuItemDto>> GetMenuAsync(int restaurantId)
    {
        var result = await _httpClient
            .GetFromJsonAsync<ApiResponse<List<MenuItemDto>>>(
                $"api/restaurants/{restaurantId}/menu");

        return result?.Data ?? new List<MenuItemDto>();
    }
}