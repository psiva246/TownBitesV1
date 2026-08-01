namespace TownBites.AdminWeb.ViewModels;

public class PageHeaderViewModel
{
    public string Title { get; set; } = "";

    public string? SubTitle { get; set; }

    public string? ButtonText { get; set; }

    public string? ButtonUrl { get; set; }

    public string ButtonIcon { get; set; } = "fa fa-plus";
}