using TownBites.CustomerApp.Views.Orders;

namespace TownBites.CustomerApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Views.Home.HomePage), typeof(Views.Home.HomePage));
        Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
        Routing.RegisterRoute(nameof(Views.Cart.CartPage), typeof(Views.Cart.CartPage));
        Routing.RegisterRoute(nameof(Views.Menu.MenuPage), typeof(Views.Menu.MenuPage));
        Routing.RegisterRoute(nameof(Views.Checkout.CheckoutPage), typeof(Views.Checkout.CheckoutPage));
        Routing.RegisterRoute(nameof(Views.Orders.OrderSuccessPage), typeof(Views.Orders.OrderSuccessPage));
        Routing.RegisterRoute(nameof(Views.Orders.OrdersPage), typeof(Views.Orders.OrdersPage));
    }
}