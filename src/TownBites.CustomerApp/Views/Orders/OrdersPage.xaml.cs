using TownBites.CustomerApp.ViewModels;

namespace TownBites.CustomerApp.Views.Orders;

public partial class OrdersPage : ContentPage
{
    private readonly OrdersViewModel _viewModel;

    public OrdersPage(OrdersViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadOrdersCommand.ExecuteAsync(null);
    }
}