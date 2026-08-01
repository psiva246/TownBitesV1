using TownBites.Shared.Enums;

namespace TownBites.Shared.Contracts.Responses;

public class OrderResponse
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RestaurantId { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime OrderedOn { get; set; }

    public List<OrderItemResponse> Items { get; set; } = new();
}

public class OrderItemResponse
{
    public int MenuItemId { get; set; }

    public string ItemName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
}