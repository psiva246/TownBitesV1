using TownBites.CustomerApp.ViewModels;

namespace TownBites.CustomerApp.Views.Cart;

public partial class CartPage : ContentPage
{
    private readonly CartViewModel _viewModel;

    public CartPage(CartViewModel vm)
    {
        InitializeComponent();

        BindingContext = _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadCartCommand.ExecuteAsync(null);
    }
}