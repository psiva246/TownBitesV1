namespace TownBites.CustomerApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Views.Home.HomePage), typeof(Views.Home.HomePage));
        Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
    }
}