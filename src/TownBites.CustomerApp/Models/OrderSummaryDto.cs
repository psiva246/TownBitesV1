using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownBites.CustomerApp.Models
{
    public class OrderSummaryDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = "";
        public string RestaurantName { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "";
        public DateTime OrderedOn { get; set; }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = "";
        public string RestaurantName { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "";
        public DateTime OrderedOn { get; set; }
    }

    public class OrderDetailsDto
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = "";

        public string RestaurantName { get; set; } = "";

        public string DeliveryAddress { get; set; } = "";

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "";

        public DateTime OrderedOn { get; set; }

        public List<OrderItemDto> Items { get; set; } = new();
    }
    public class OrderItemDto
    {
        public string ItemName { get; set; } = "";

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total => Quantity * Price;
    }
}
