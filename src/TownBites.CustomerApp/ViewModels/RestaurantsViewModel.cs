using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.ViewModels;

public partial class RestaurantsViewModel : ObservableObject
{
    private readonly IRestaurantService _restaurantService;

    public ObservableCollection<RestaurantDto> Restaurants { get; } = new();

    [ObservableProperty]
    bool isBusy;

    public RestaurantsViewModel(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [RelayCommand]
    private async Task LoadRestaurants()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            Restaurants.Clear();

            var result = await _restaurantService.GetRestaurantsAsync();

            foreach (var restaurant in result)
            {
                Restaurants.Add(restaurant);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
}