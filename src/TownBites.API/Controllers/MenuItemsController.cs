using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using TownBites.Application.Interfaces;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Requests;
using System.Security.Claims;
using TownBites.API.Helpers;

namespace TownBites.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        /// <summary>
        /// Get all menu items
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            TownBites.Shared.Common.ApiResponse<List<MenuItemDto>> result = new TownBites.Shared.Common.ApiResponse<List<MenuItemDto>>();
            var restaurantClaim = UserClaimsHelper.GetRestaurantId(User);
            if (restaurantClaim != null)
                result = await _menuItemService.GetByRestaurantAsync(Convert.ToInt32(restaurantClaim));
            else
                result = await _menuItemService.GetAllAsync();

            return Ok(result);
        }

        /// <summary>
        /// Get menu item by Id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _menuItemService.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Get menu items by category
        /// </summary>
        [HttpGet("category/{categoryId:int}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var result = await _menuItemService.GetByCategoryAsync(categoryId);

            return Ok(result);
        }

        /// <summary>
        /// Get menu items by restaurant
        /// </summary>
        [HttpGet("restaurant/{restaurantId:int}")]
        public async Task<IActionResult> GetByRestaurant(int restaurantId)
        {
            var result = await _menuItemService.GetByRestaurantAsync(restaurantId);

            return Ok(result);
        }

        /// <summary>
        /// Create menu item
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreateMenuItemRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            MenuItemDto createRequest = new MenuItemDto
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                //DiscountPrice = request.DiscountPrice,
                //IsVeg = request.IsVeg,
                IsAvailable = request.IsAvailable,
                //PreparationTimeInMinutes = request.PreparationTimeInMinutes,
                ImageUrl = request.ImageUrl
            };

            var result = await _menuItemService.CreateAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Update menu item
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateMenuItemRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _menuItemService.UpdateAsync(id, request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Delete menu item
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _menuItemService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        ///// <summary>
        ///// Change availability
        ///// </summary>
        //[HttpPatch("{id:int}/availability")]
        //public async Task<IActionResult> ChangeAvailability(int id, [FromBody] bool isAvailable)
        //{
        //    var result = await _menuItemService.ChangeAvailabilityAsync(id, isAvailable);

        //    if (!result.Success)
        //        return BadRequest(result);

        //    return Ok(result);
        //}
    }
}