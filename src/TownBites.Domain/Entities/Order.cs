using TownBites.Shared.Enums;

namespace TownBites.Domain.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderedOn { get; set; }    
    public Restaurant Restaurant { get; set; } = null!;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}