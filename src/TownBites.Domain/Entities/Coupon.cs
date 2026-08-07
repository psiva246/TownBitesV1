using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownBites.Domain.Entities
{
    public class Coupon
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public string Code { get; set; } = "";

        public decimal DiscountValue { get; set; }

        public bool IsPercentage { get; set; }

        public decimal MinimumOrderAmount { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public bool IsActive { get; set; }
    }
}
