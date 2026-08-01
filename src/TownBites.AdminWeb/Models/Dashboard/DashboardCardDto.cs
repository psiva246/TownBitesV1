namespace TownBites.AdminWeb.Models.Dashboard
{
    public class DashboardCardDto
    {
        public int OrdersToday { get; set; }

        public decimal RevenueToday { get; set; }

        public int Customers { get; set; }

        public int ActiveMenuItems { get; set; }
    }
}
