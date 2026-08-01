namespace TownBites.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }

    public int MenuItemId { get; set; }

    public string ItemName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public Order Order { get; set; } = null!;
    public MenuItem MenuItem { get; set; } = null!;
}