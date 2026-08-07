using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownBites.Shared.Enums;

namespace TownBites.Shared.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string RestaurantName { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime OrderedOn { get; set; }
    }

    public class OrderDetailsDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public string Address { get; set; } = "";

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "";

        //public List<OrderItemDto> Items { get; set; } = new();
        public List<OrderDto> Items { get; set; } = new();
    }
}
