namespace TownBites.Domain.Entities;

public class Cart : BaseEntity
{
    public int UserId { get; set; }

    public bool IsCheckedOut { get; set; }

    public ICollection<CartItem> Items { get; set; }
        = new List<CartItem>();
}