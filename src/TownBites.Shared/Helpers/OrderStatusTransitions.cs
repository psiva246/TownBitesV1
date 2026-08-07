using TownBites.Shared.Enums;

namespace TownBites.Application.Common;

public static class OrderStatusTransitions
{
    private static readonly Dictionary<OrderStatus, OrderStatus[]> AllowedTransitions =
        new()
        {
            {
                OrderStatus.Pending,
                new[]
                {
                    OrderStatus.Accepted,
                    OrderStatus.Rejected,
                    OrderStatus.Cancelled
                }
            },
            {
                OrderStatus.Accepted,
                new[]
                {
                    OrderStatus.Preparing,
                    OrderStatus.Cancelled
                }
            },
            {
                OrderStatus.Preparing,
                new[]
                {
                    OrderStatus.Ready,
                    OrderStatus.Cancelled
                }
            },
            {
                OrderStatus.Ready,
                new[]
                {
                    OrderStatus.OutForDelivery,
                    OrderStatus.Delivered
                }
            },
            {
                OrderStatus.OutForDelivery,
                new[]
                {
                    OrderStatus.Delivered
                }
            },
            {
                OrderStatus.Delivered,
                Array.Empty<OrderStatus>()
            },
            {
                OrderStatus.Cancelled,
                Array.Empty<OrderStatus>()
            },
            {
                OrderStatus.Rejected,
                Array.Empty<OrderStatus>()
            }
        };

    /// <summary>
    /// Returns true if the status transition is allowed.
    /// </summary>
    public static bool CanTransition(
        OrderStatus currentStatus,
        OrderStatus newStatus)
    {
        if (!AllowedTransitions.TryGetValue(currentStatus, out var allowed))
            return false;

        return allowed.Contains(newStatus);
    }

    /// <summary>
    /// Returns the next valid statuses for the current status.
    /// </summary>
    public static IReadOnlyList<OrderStatus> GetAllowedTransitions(
        OrderStatus currentStatus)
    {
        if (!AllowedTransitions.TryGetValue(currentStatus, out var allowed))
            return Array.Empty<OrderStatus>();

        return allowed;
    }

    /// <summary>
    /// Returns true if the order is completed.
    /// </summary>
    public static bool IsCompleted(OrderStatus status)
    {
        return status == OrderStatus.Delivered;
    }

    /// <summary>
    /// Returns true if the order is closed.
    /// </summary>
    public static bool IsClosed(OrderStatus status)
    {
        return status == OrderStatus.Delivered
            || status == OrderStatus.Cancelled
            || status == OrderStatus.Rejected;
    }

    /// <summary>
    /// Returns true if the order can still be cancelled.
    /// </summary>
    public static bool CanCancel(OrderStatus status)
    {
        return status == OrderStatus.Pending
            || status == OrderStatus.Accepted
            || status == OrderStatus.Preparing;
    }
}