namespace TownBites.Shared.Contracts.Requests;

public class AddCartItemRequest
{
    public int MenuItemId { get; set; }

    public int Quantity { get; set; }
}

public class UpdateCartItemRequest
{
    public int Quantity { get; set; }
}