namespace TownBites.CustomerApp.Models;

public class CartDto
{
    public List<CartItemDto> Items { get; set; } = new();

    public decimal GrandTotal => Items.Sum(x => x.Total);

    public int TotalItems => Items.Sum(x => x.Quantity);
}

public class CartItemDto
{
    public int MenuItemId { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal Total => Price * Quantity;

    public string? ImageUrl { get; set; }
}