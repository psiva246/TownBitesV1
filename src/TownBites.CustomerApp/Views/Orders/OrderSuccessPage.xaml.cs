using TownBites.CustomerApp.ViewModels;

namespace TownBites.CustomerApp.Views.Orders;

public partial class OrderSuccessPage : ContentPage
{
    public OrderSuccessPage(OrderSuccessViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}