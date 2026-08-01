using System.ComponentModel.DataAnnotations;

public class CategoryFormViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public string? Description { get; set; }
}