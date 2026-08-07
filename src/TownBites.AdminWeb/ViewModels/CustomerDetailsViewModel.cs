using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models.Customer;

public partial class CustomerDetailsViewModel : ObservableObject
{
    private readonly ICustomerApiService _service;

    [ObservableProperty]
    private CustomerDetailsDto? customer;

    public CustomerDetailsViewModel(ICustomerApiService service)
    {
        _service = service;
    }

    public async Task Load(int id)
    {
        customer = await _service.GetCustomerAsync(id);
    }
}