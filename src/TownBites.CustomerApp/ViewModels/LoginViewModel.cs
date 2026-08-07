using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using TownBites.CustomerApp.Helpers;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthApiService _authApiService;
    [ObservableProperty]
    private string email = "testuser1@gmail.com"; // string.Empty;
    [ObservableProperty]
    private string password = "Password@001"; // string.Empty;
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
                PreferencesHelper.SaveToken(response.token);
            }

            //await Shell.Current.DisplayAlert( "Success", "Login Successful", "OK");

            await Shell.Current.GoToAsync("//Restaurants");
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