using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TownBites.CustomerApp.Helpers;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;
using System.Collections.ObjectModel;

namespace TownBites.CustomerApp.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    private readonly IMenuService _menuService;

    public ObservableCollection<MenuItemDto> MenuItems { get; } = new();

    public MenuViewModel(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [RelayCommand]
    private async Task LoadMenu()
    {
        MenuItems.Clear();

        var restaurantId = Preferences.Default.Get("RestaurantId", 0);

        var items = await _menuService.GetMenuAsync(restaurantId);

        foreach (var item in items)
            MenuItems.Add(item);
    }
}