using Microsoft.AspNetCore.SignalR;

namespace TownBites.Infrastructure.Hubs;

public class OrderHub : Hub
{
    public async Task JoinRestaurant(string restaurantId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"restaurant-{restaurantId}");
    }

    public async Task LeaveRestaurant(string restaurantId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"restaurant-{restaurantId}");
    }
}