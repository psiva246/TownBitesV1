using Microsoft.AspNetCore.Mvc;
using TownBites.AdminWeb.Interfaces;
using TownBites.Shared.Contracts.Requests;
using TownBites.Application.Interfaces;

namespace TownBites.AdminWeb.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly ICategoryApiService _categoryService;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            //_categoryService = categoryService;
            _categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index(int RestaurantId)
        {
            //var restaurantId = User.FindFirst("RestaurantId")?.Value;
            RestaurantId = 2;
            var result = await _categoryService.GetAllAsync(RestaurantId);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View(new List<TownBites.Shared.Contracts.Responses.CategoryResponse>());
            }

            List<TownBites.AdminWeb.Models.Category.CategoryDto> lstCategories = new List<Models.Category.CategoryDto>();
            foreach (var item in result.Data)
            {
                lstCategories.Add(new Models.Category.CategoryDto() { Id = item.Id, RestaurantId = item.RestaurantId, Name = item.Name
                        , Description = item.Description, DisplayOrder = item.DisplayOrder, IsActive = item.IsActive });
            }
            
            return View(lstCategories);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View(new TownBites.Application.Requests.CreateCategoryRequest());
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TownBites.Application.Requests.CreateCategoryRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _categoryService.CreateAsync(model);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            TempData["Success"] = "Category created successfully.";

            return RedirectToAction(nameof(Index), new { RestaurantId = model.RestaurantId });
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (!result.Success || result.Data == null)
                return NotFound();

            var model = new CategoryRequest
            {
                Name = result.Data.Name,
                Description = result.Data.Description
            };

            return View(model);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TownBites.Application.Requests.UpdateCategoryRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _categoryService.UpdateAsync(id, model);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            TempData["Success"] = "Category updated successfully.";

            //return RedirectToAction(nameof(Index), new { RestaurantId = model.RestaurantId });
            return RedirectToAction(nameof(Index), new { RestaurantId = 2 });
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
            }
            else
            {
                TempData["Success"] = "Category deleted successfully.";
            }

            return RedirectToAction(nameof(Index), new { RestaurantId = 0 });
        }
    }
}