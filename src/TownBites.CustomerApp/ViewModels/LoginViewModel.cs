using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;
using Microsoft.Maui.Storage;

namespace TownBites.CustomerApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthApiService _authApiService;
    [ObservableProperty]
    private string email = string.Empty;
    [ObservableProperty]
    private string password = string.Empty;
    [ObservableProperty]
    private bool isBusy;

    public LoginViewModel(IAuthApiService authApiService)
    {
        _authApiService = authApiService;
    }

    [RelayCommand]
    private async Task Login()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            var response = await _authApiService.LoginAsync(new LoginRequest
            {
                Email = Email,
                Password = Password
            });

            if (response == null)
            {
                await Shell.Current.DisplayAlert("Login", "Invalid credentials", "OK");
                return;
            }
            if (!string.IsNullOrWhiteSpace(response.token))
            {
                Preferences.Default.Set("jwt", response.token);
            }

            //await Shell.Current.DisplayAlert( "Success", "Login Successful", "OK");

            // Navigation comes next commit
            Preferences.Default.Set("jwt", response.token);

            await Shell.Current.GoToAsync("//Home");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}