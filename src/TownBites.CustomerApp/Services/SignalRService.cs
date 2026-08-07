using Microsoft.AspNetCore.SignalR.Client;

public class SignalRService
{
    private HubConnection? _connection;

    public async Task StartAsync(string token)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7276/orderHub", options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(token)!;
            })
            .WithAutomaticReconnect()
            .Build();

        await _connection.StartAsync();
    }

    public async Task JoinOrderAsync(int orderId)
    {
        if (_connection != null)
            await _connection.InvokeAsync("JoinRestaurant", orderId.ToString());
    }

    public async Task JoinOrder(string orderId)
    {
        //await Groups.AddToGroupAsync(Context.ConnectionId, $"order-{orderId}");
    }
}