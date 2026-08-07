using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;
using TownBites.CustomerApp.Views.Orders;

namespace TownBites.CustomerApp.ViewModels;

public partial class CheckoutViewModel : ObservableObject
{
    private readonly IOrderService _orderService;

    [ObservableProperty]
    private string deliveryAddress = "";

    [ObservableProperty]
    private decimal grandTotal;

    [ObservableProperty]
    private string selectedPaymentMethod = "Cash";

    public List<string> PaymentMethods { get; } =
    [
        "Cash",
        "UPI",
        "Card"
    ];

    public CheckoutViewModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [RelayCommand]
    private async Task PlaceOrder()
    {
        var request = new CheckoutRequest
        {
            DeliveryAddress = DeliveryAddress,
            PaymentMethod = SelectedPaymentMethod
        };

        var result = await _orderService.CheckoutAsync(request);

        if (!result.Success)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                result.Message,
                "OK");

            return;
        }
        //await Shell.Current.GoToAsync($"//OrderSuccess?orderId={result.Data}");
        await Shell.Current.GoToAsync(nameof(OrderSuccessPage));
    }
}