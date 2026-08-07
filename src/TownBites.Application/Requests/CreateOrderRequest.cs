using System.ComponentModel.DataAnnotations;

namespace TownBites.Application.Requests;

public class CreateOrderRequest
{
    [Required]
    public int RestaurantId { get; set; }

    [Required]
    [MaxLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string CustomerPhone { get; set; } = string.Empty;

    [Required]
    public decimal TotalAmount { get; set; }
}