using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.Interfaces;

public interface IRestaurantService
{
    Task<List<RestaurantDto>> GetRestaurantsAsync();
}