using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownBites.Shared.Enums;

namespace TownBites.Shared.Helpers
{
    public static class OrderStatusTransitions
    {
        public static readonly Dictionary<OrderStatus, OrderStatus[]> Allowed =
            new()
            {
            { OrderStatus.Pending, new[]
                {
                    OrderStatus.Accepted,
                    OrderStatus.Cancelled
                }
            },

            { OrderStatus.Accepted, new[]
                {
                    OrderStatus.Preparing
                }
            },

            { OrderStatus.Preparing, new[]
                {
                    OrderStatus.Ready
                }
            },

            { OrderStatus.Ready, new[]
                {
                    OrderStatus.PickedUp
                }
            },

            { OrderStatus.PickedUp, new[]
                {
                    OrderStatus.Delivered
                }
            }
            };
    }
}
