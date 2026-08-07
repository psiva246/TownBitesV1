using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TownBites.CustomerApp.Interfaces;
using TownBites.CustomerApp.Models;

namespace TownBites.CustomerApp.ViewModels;

[QueryProperty(nameof(OrderId), "orderId")]
public partial class OrderDetailsViewModel : ObservableObject
{
    private readonly IOrderService _orderService;

    [ObservableProperty]
    private int orderId;

    [ObservableProperty]
    private string orderNumber = "";

    [ObservableProperty]
    private string restaurantName = "";

    [ObservableProperty]
    private string deliveryAddress = "";

    [ObservableProperty]
    private decimal totalAmount;

    [ObservableProperty]
    private string status = "";

    public ObservableCollection<OrderItemDto> Items { get; } = new();

    public OrderDetailsViewModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [RelayCommand]
    public async Task LoadOrder()
    {
        if (OrderId == 0)
            return;

        var order = await _orderService.GetOrderDetailsAsync(OrderId);

        if (order == null)
            return;

        OrderNumber = order.OrderNumber;
        RestaurantName = order.RestaurantName;
        DeliveryAddress = order.DeliveryAddress;
        TotalAmount = order.TotalAmount;
        Status = order.Status;

        Items.Clear();

        foreach (var item in order.Items)
            Items.Add(item);
    }
}