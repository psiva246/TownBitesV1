using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;
using TownBites.CustomerApp.Views.Orders;

namespace TownBites.CustomerApp.ViewModels;

public partial class OrdersViewModel : ObservableObject
{
    private readonly IOrderService _orderService;

    public ObservableCollection<OrderDto> Orders { get; } = new();

    [ObservableProperty]
    private bool isBusy;

    public OrdersViewModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [RelayCommand]
    private async Task LoadOrders()
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            Orders.Clear();

            var orders = await _orderService.GetMyOrdersAsync();

            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task OpenOrder(OrderDto order)
    {
        if (order == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(OrderDetailsPage)}?orderId={order.Id}");
    }
}