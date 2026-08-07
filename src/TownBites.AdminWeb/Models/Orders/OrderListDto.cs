namespace TownBites.AdminWeb.Models.Orders
{
    public class OrderListDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "";

        public DateTime OrderedOn { get; set; }
    }

    public class OrderSummaryDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "";

        public DateTime OrderedOn { get; set; }
    }
}
