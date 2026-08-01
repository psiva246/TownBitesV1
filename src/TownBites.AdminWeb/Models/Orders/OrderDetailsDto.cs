namespace TownBites.AdminWeb.Models.Orders
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public string Address { get; set; } = "";

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "";

        //public List<OrderItemDto> Items { get; set; } = new();
        public List<OrderListDto> Items { get; set; } = new();
    }
}
