namespace TownBites.CustomerApp.Models;

public class CheckoutRequest
{
    public string DeliveryAddress { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = "Cash";
}