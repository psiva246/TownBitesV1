using TownBites.Shared.Enums;

namespace TownBites.Shared.Contracts.Requests;

public class UpdateOrderStatusRequest
{
    public OrderStatus Status { get; set; }
}