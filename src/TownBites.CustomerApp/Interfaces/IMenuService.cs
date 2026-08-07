using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.Interfaces;

public interface IMenuService
{
    Task<List<MenuItemDto>> GetMenuAsync(int restaurantId);
}