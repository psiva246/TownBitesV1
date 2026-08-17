using TownBites.Shared.Enums;

namespace TownBites.Domain.Entities;

public class Order : BaseEntity
{
    public int RestaurantId { get; set; }

    public int CustomerId { get; set; }

    public decimal SubTotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal DeliveryCharge { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentMethod { get; set; } = "";

    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime OrderDate { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
