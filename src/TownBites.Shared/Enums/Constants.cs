using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownBites.Shared.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Accepted = 2,
        Preparing = 3,
        Ready = 4,
        Delivered = 5,
        Cancelled = 6
    }
    public enum UserRole
    {
        Admin = 1,
        Restaurant = 2,
        Customer = 3
    }
}
