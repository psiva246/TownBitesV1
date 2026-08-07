using TownBites.Shared.Enums;

namespace TownBites.Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }

    public int RestaurantId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerPhone { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public DateTime OrderDate { get; set; }
}