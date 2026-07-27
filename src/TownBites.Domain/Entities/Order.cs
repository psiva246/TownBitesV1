using TownBites.Shared.Enums;

namespace TownBites.Domain.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }

    public decimal TotalAmount { get; set; }

    //public string Status { get; set; } //= "Pending";
    public OrderStatus Status { get; set; }
    public DateTime OrderedOn { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}