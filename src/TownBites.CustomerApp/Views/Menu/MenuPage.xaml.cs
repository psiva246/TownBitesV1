using TownBites.CustomerApp.ViewModels;

namespace TownBites.CustomerApp.Views.Menu;

public partial class MenuPage : ContentPage
{	
    private readonly MenuViewModel _viewModel;

    public MenuPage(MenuViewModel vm)
    {
        InitializeComponent();

        BindingContext = _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadMenuCommand.ExecuteAsync(null);
    }
}