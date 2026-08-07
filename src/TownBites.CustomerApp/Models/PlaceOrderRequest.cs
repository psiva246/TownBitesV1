namespace TownBites.CustomerApp.Models;

public class OrderItemRequest
{
    public int MenuItemId { get; set; }

    public int Quantity { get; set; }
}

public class PlaceOrderRequest
{
    public int RestaurantId { get; set; }

    public int CustomerId { get; set; }

    public string PaymentMethod { get; set; } = "";

    public decimal SubTotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal DeliveryCharge { get; set; }

    public decimal TotalAmount { get; set; }

    public List<PlaceOrderItemRequest> Items { get; set; } = new();
}

public class PlaceOrderItemRequest
{
    public int MenuItemId { get; set; }

    public int Quantity { get; set; }
}

public class PlaceOrderResponse
{
    public int OrderId { get; set; }

    public string OrderNumber { get; set; } = "";

    public string Status { get; set; } = "";

    public decimal Total { get; set; }

    public DateTime OrderedOn { get; set; }
}