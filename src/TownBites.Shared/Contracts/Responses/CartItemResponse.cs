namespace TownBites.Shared.Contracts.Responses;

public class CartItemResponse
{
    public int Id { get; set; }

    public int MenuItemId { get; set; }

    public string MenuItemName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal Total => UnitPrice * Quantity;
}

public class CartResponse
{
    public List<CartItemResponse> Items { get; set; }
        = new();

    public decimal GrandTotal =>
        Items.Sum(x => x.Total);
}