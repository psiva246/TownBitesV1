using System.ComponentModel.DataAnnotations;
using TownBites.Shared.Enums;

namespace TownBites.Application.Requests;

public class UpdateOrderStatusRequest
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}