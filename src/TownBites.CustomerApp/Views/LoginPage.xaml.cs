using TownBites.CustomerApp.ViewModels;

namespace TownBites.CustomerApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}