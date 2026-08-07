using TownBites.Shared.Contracts.Responses;

namespace TownBites.AdminWeb.Models.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public class CustomerDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public string Address { get; set; } = "";

        public bool IsActive { get; set; }

        public int TotalOrders { get; set; }

        public decimal TotalSpent { get; set; }

        public DateTime? LastOrderDate { get; set; }

        public List<OrderResponse> Orders { get; set; } = new();
    }
}
