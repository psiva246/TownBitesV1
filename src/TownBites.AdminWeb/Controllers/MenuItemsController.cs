using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models.Menu;

namespace TownBites.AdminWeb.Controllers;

[Authorize(Roles = "Admin,Restaurant")]
public class MenuItemsController : Controller
{
    private readonly IMenuItemApiService _menuItemApiService;

    public MenuItemsController(IMenuItemApiService menuItemApiService)
    {
        _menuItemApiService = menuItemApiService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var items = await _menuItemApiService.GetAllAsync();
            return View(items);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return View(new List<MenuItemDto>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadCategories();

        return View(new CreateMenuItemRequest());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateMenuItemRequest model)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategories();
            return View(model);
        }

        try
        {
            await _menuItemApiService.CreateAsync(model);

            TempData["Success"] = "Menu item created successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            await LoadCategories();

            ModelState.AddModelError("", ex.Message);

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var item = await _menuItemApiService.GetByIdAsync(id);

            if (item == null)
                return NotFound();

            var model = new UpdateMenuItemRequest
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CategoryId = item.CategoryId,
                IsVeg = item.IsVeg,
                IsAvailable = item.IsAvailable,
                ExistingImageUrl = item.ImageUrl
            };

            await LoadCategories();

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;

            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateMenuItemRequest model)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategories();
            return View(model);
        }

        try
        {
            await _menuItemApiService.UpdateAsync(model);

            TempData["Success"] = "Menu item updated successfully.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            await LoadCategories();

            ModelState.AddModelError("", ex.Message);

            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _menuItemApiService.DeleteAsync(id);

            TempData["Success"] = "Menu item deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCategories()
    {
        var categories = await _menuItemApiService.GetCategoriesAsync();

        ViewBag.Categories = categories
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            .ToList();
    }
}