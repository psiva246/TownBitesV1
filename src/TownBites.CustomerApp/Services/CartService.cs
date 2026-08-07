using System.Net.Http.Json;
using System.Text.Json;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.Services;

public class CartService : ICartService
{
    private const string CartKey = "cart";
    private readonly HttpClient _httpClient;

    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<CartItemDto>> GetCartAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<CartItemDto>>>("api/cart");

        return response?.Data ?? new();
    }

    public async Task AddToCartAsync(MenuItemDto item)
    {
        var cart = await GetCartAsync();

        var existing =
            cart.FirstOrDefault(x => x.MenuItemId == item.Id);

        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            cart.Add(new CartItemDto
            {
                MenuItemId = item.Id,
                Name = item.Name,
                Price = item.Price,
                Quantity = 1
            });
        }

        SaveCart(cart);
    }

    public async Task IncreaseQuantityAsync(int id)
    {
        await _httpClient.PutAsync($"api/cart/{id}/increase", null);
    }

    public async Task DecreaseQuantityAsync(int id)
    {
        await _httpClient.PutAsync($"api/cart/{id}/decrease", null);
    }

    public async Task RemoveItemAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/cart/{id}");
    }

    public Task ClearCartAsync()
    {
        Preferences.Default.Remove(CartKey);

        return Task.CompletedTask;
    }

    private void SaveCart(List<CartItemDto> cart)
    {
        Preferences.Default.Set(CartKey, JsonSerializer.Serialize(cart));
    }
}