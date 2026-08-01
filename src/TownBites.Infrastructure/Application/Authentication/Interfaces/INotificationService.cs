namespace TownBites.Infrastructure.Interfaces;

public interface INotificationService
{
    Task SendToGroupAsync(string groupName, string eventName, object data);
    Task NotifyNewOrderAsync(int restaurantId, object payload);
    Task NotifyOrderUpdatedAsync(int restaurantId, object payload);
}