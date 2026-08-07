using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.Interfaces;

public interface ICartService
{
    Task AddToCartAsync(MenuItemDto item);

    Task<List<CartItemDto>> GetCartAsync();

    Task ClearCartAsync();

    Task IncreaseQuantityAsync(int menuItemId);

    Task DecreaseQuantityAsync(int menuItemId);

    Task RemoveItemAsync(int menuItemId);
}