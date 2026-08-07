using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models.Customer;

public partial class CustomerViewModel : ObservableObject
{
    private readonly ICustomerApiService _service;

    [ObservableProperty]
    private List<CustomerDto> customers = new();

    public CustomerViewModel(ICustomerApiService service)
    {
        _service = service;
    }

    [RelayCommand]
    private async Task LoadCustomers()
    {
        customers = await _service.GetCustomersAsync();
    }
}