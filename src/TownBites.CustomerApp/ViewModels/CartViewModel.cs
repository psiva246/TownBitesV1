using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;
using TownBites.CustomerApp.Views.Checkout;

namespace TownBites.CustomerApp.ViewModels;

public partial class CartViewModel : ObservableObject
{
    private readonly ICartService _cartService;
    private readonly IMenuService _menuService;

    public ObservableCollection<CartItemDto> Items { get; } = new();

    [ObservableProperty]
    private decimal grandTotal;

    [ObservableProperty]
    private int totalItems;

    [ObservableProperty]
    private List<CartItemDto> cartItems = new();
    public CartViewModel(ICartService cartService, IMenuService menuService)
    {
        _cartService = cartService;
        _menuService = menuService;
    }

    [RelayCommand]
    private async Task LoadCart()
    {
        cartItems = await _cartService.GetCartAsync();

        CalculateTotal();
    }

    [RelayCommand]
    private async Task IncreaseQuantity(CartItemDto item)
    {
        await _cartService.IncreaseQuantityAsync(item.MenuItemId);

        await LoadCart();
    }

    [RelayCommand]
    private async Task DecreaseQuantity(CartItemDto item)
    {
        await _cartService.DecreaseQuantityAsync(item.MenuItemId);

        await LoadCart();
    }

    [RelayCommand]
    private async Task RemoveItem(CartItemDto item)
    {
        await _cartService.RemoveItemAsync(item.MenuItemId);

        await LoadCart();
    }

    [RelayCommand]
    private async Task Checkout()
    {
        await Shell.Current.GoToAsync(nameof(CheckoutPage));
    }

    [RelayCommand]
    private async Task OpenCart()
    {
        await Shell.Current.GoToAsync(nameof(Views.Cart.CartPage));
    }

    private void CalculateTotal()
    {
        GrandTotal = CartItems.Sum(x => x.Price * x.Quantity);
    }
}