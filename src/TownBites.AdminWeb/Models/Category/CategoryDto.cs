namespace TownBites.AdminWeb.Models.Category;

public class CategoryDto
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public int DisplayOrder { get; set; }
}

public class CategoryModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

