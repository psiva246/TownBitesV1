using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TownBites.AdminWeb.Interfaces;
using TownBites.AdminWeb.Models;
using TownBites.AdminWeb.Models.Menu;
using TownBites.Shared.Contracts.Requests;

namespace TownBites.AdminWeb.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly IMenuItemApiService _menuItemService;
        private readonly ICategoryApiService _categoryService;
        private readonly IUploadApiService _uploadService;
        private readonly IOptions<ApiSettings> _options;
        public MenuItemsController(IMenuItemApiService menuItemService, ICategoryApiService categoryService, IUploadApiService uploadService, IOptions<ApiSettings> options)
        {
            _menuItemService = menuItemService;
            _categoryService = categoryService;
            _uploadService = uploadService;
            _options = options;
        }

        // GET: /MenuItems
        public async Task<IActionResult> Index()
        {
            var response = await _menuItemService.GetAllAsync();

            if (!response.Success)
            {
                TempData["Error"] = response.Message;
                return View(new List<TownBites.Shared.Contracts.Requests.MenuItemDto>());
            }

            return View(response.Data);
        }

        // GET: /MenuItems/Create
        public async Task<IActionResult> Create(int restaurentID)
        {
            await LoadCategories(restaurentID);

            return View(new MenuItemRequest());
        }

        // POST: /MenuItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategories(request.RestaurantId);
                return View(request);
            }

            //CreateMenuItemRequest createRequest = new CreateMenuItemRequest
            //{
            //    //RestaurantId = request.RestaurantId,
            //    CategoryId = request.CategoryId,
            //    Name = request.Name,
            //    Description = request.Description,
            //    Price = request.Price,
            //    IsAvailable = request.IsAvailable,
            //    //ImageUrl = request.ImageUrl
            //};

            var result = await _menuItemService.CreateAsync(request);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                await LoadCategories(request.RestaurantId);
                return View(request);
            }

            TempData["Success"] = result.Message;

            return RedirectToAction(nameof(Index));
        }

        // GET: /MenuItems/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _menuItemService.GetByIdAsync(id);

            if (!response.Success || response.Data == null)
                return NotFound();

            await LoadCategories(response.Data.RestaurantId);

            var model = new MenuItemRequest
            {
                Name = response.Data.Name,
                Description = response.Data.Description,
                Price = response.Data.Price,
                DiscountPrice = response.Data.DiscountPrice,
                CategoryId = response.Data.CategoryId,
                RestaurantId = response.Data.RestaurantId,
                IsAvailable = response.Data.IsAvailable,
                ExistingImageUrl = response.Data.ExistingImageUrl,
                IsVeg = response.Data.IsVeg,
            };

            return View(model);
        }

        // POST: /MenuItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategories(request.RestaurantId);
                return View(request);
            }

            if (request.ImageUrl != null)
            {
                string imageurl = await _uploadService.UploadImageAsync(request.ImageUrl);
                request.uploadedImageUrl = _options.Value.BaseUrl + imageurl;
            }
            else
            {
                request.uploadedImageUrl = request.ExistingImageUrl;
            }
            var result = await _menuItemService.UpdateAsync(id, request);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                await LoadCategories(request.RestaurantId);
                return View(request);
            }

            TempData["Success"] = result.Message;

            return RedirectToAction(nameof(Index));
        }

        // GET: /MenuItems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _menuItemService.GetByIdAsync(id);

            if (!response.Success || response.Data == null)
                return NotFound();

            return View(response.Data);
        }

        // POST: /MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _menuItemService.DeleteAsync(id);

            TempData[result.Success ? "Success" : "Error"] = result.Message;

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadCategories(int restaurantId)
        {
            var categories = await _categoryService.GetAllAsync(restaurantId);
            List<TownBites.AdminWeb.Models.Category.CategoryDto> categoryLst = new List<TownBites.AdminWeb.Models.Category.CategoryDto>();
            categoryLst.Add(new TownBites.AdminWeb.Models.Category.CategoryDto
            {
                Id = 0,
                Name = "Select Category"
            });
            foreach (var item in categories.Data) {
                TownBites.AdminWeb.Models.Category.CategoryDto categoryDto = new TownBites.AdminWeb.Models.Category.CategoryDto
                {
                    Id = item.Id,
                    Name = item.Name
                };
                categoryLst.Add(categoryDto);
            }

            ViewBag.Categories = categoryLst ?? new List<TownBites.AdminWeb.Models.Category.CategoryDto>();
        }
    }
}