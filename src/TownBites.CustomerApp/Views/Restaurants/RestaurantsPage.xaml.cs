using TownBites.CustomerApp.ViewModels;

namespace TownBites.CustomerApp.Views.Restaurants;

public partial class RestaurantsPage : ContentPage
{
    private readonly RestaurantsViewModel _viewModel;

    public RestaurantsPage(RestaurantsViewModel vm)
    {
        InitializeComponent();

        BindingContext = _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadRestaurantsCommand.ExecuteAsync(null);
    }
}