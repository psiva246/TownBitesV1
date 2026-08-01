using Microsoft.AspNetCore.SignalR;
using TownBites.Infrastructure.Interfaces;
using TownBites.Infrastructure.Hubs;

namespace TownBites.Infrastructure.Services;

public class SignalRNotificationService : INotificationService
{
    private readonly IHubContext<OrderHub> _hubContext;

    public SignalRNotificationService(IHubContext<OrderHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendToGroupAsync(string groupName, string eventName, object data)
    {
        await _hubContext.Clients.Group(groupName).SendAsync(eventName, data);
    }

    public async Task NotifyNewOrderAsync(int restaurantId, object payload)
    {
        await SendToGroupAsync($"restaurant-{restaurantId}", "NewOrder", payload);
    }

    public async Task NotifyOrderUpdatedAsync(int restaurantId, object payload)
    {
        await SendToGroupAsync($"restaurant-{restaurantId}", "OrderUpdated", payload);
    }
}