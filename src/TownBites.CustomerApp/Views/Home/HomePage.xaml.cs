namespace TownBites.CustomerApp.Views.Home;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void Logout_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Remove("jwt");
        await Shell.Current.GoToAsync("//Login");
    }
}