using System.ComponentModel.DataAnnotations;

namespace TownBites.Application.Requests;

public class CreateRestaurantRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;
}