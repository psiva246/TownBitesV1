using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TownBites.CustomerApp.ViewModels;

public partial class OrderSuccessViewModel : ObservableObject
{
    [ObservableProperty]
    private string orderMessage = "";

    [ObservableProperty]
    private string estimatedTime = "";

    public OrderSuccessViewModel()
    {
        OrderMessage = "Your order has been received by the restaurant.";

        EstimatedTime = "Estimated Delivery: 30-45 Minutes";
    }

    [RelayCommand]
    private async Task ContinueShopping()
    {
        await Shell.Current.GoToAsync("//RestaurantsPage");
    }

    [RelayCommand]
    private async Task ViewOrders()
    {
        await Shell.Current.GoToAsync(nameof(Views.Orders.OrderSuccessPage));
    }
}