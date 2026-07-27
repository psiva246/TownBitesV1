namespace TownBites.Domain.Entities;

public class CartItem : BaseEntity
{
    public int CartId { get; set; }

    public int MenuItemId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public Cart Cart { get; set; } = null!;

    public MenuItem MenuItem { get; set; } = null!;
}