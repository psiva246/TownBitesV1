//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using TownBites.Application.DTOs;
//using TownBites.Application.Interfaces;
//using TownBites.Domain.Entities;
//using TownBites.Shared.Contracts.Responses;

//using TownBites.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TownBites.Infrastructure.Interfaces;
using TownBites.Shared.Contracts.Responses;
using TownBites.API.Helpers;
using TownBites.Application.DTOs;
using TownBites.Application.Interfaces;
using TownBites.Application.Requests;

namespace TownBites.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly TownBites.Infrastructure.Interfaces.ICategoryService _categoryService;
        private readonly TownBites.Application.Interfaces.ICategoryService _applicationCategoryService;

        public CategoriesController(TownBites.Infrastructure.Interfaces.ICategoryService categoryService, TownBites.Application.Interfaces.ICategoryService applicationCategoryService)
        {
            _categoryService = categoryService;
            _applicationCategoryService = applicationCategoryService;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurantId = 2; // int.Parse(User.FindFirst("RestaurantId")?.Value);
            //var result1 = await _applicationCategoryService.GetAllAsync(restaurantId);

            var result = await _categoryService.GetAllAsync(restaurantId);

            //return Ok(new ApiResponse<IEnumerable<CategoryDto>>
            //{
            //    Success = true,
            //    Message = "Categories loaded successfully.",
            //    Data = result
            //});
            return Ok(result);
        }

        /// <summary>
        /// Get category by Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _applicationCategoryService.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Create category
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            TownBites.Application.Requests.CreateCategoryRequest createReq = new TownBites.Application.Requests.CreateCategoryRequest()
            {
                Description = request.Description, //DisplayOrder = request.DisplayOrder,
                IsActive = request.IsActive,
                Name = request.Name,
                RestaurantId = request.RestaurantId
            };
            var result = await _applicationCategoryService.CreateAsync(createReq);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Update category
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryRequest request)
        {
            TownBites.Application.Requests.UpdateCategoryRequest updateReq = new TownBites.Application.Requests.UpdateCategoryRequest()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description, //DisplayOrder = request.DisplayOrder,
                IsActive = request.IsActive,
                DisplayOrder = request.DisplayOrder
            };
            var result = await _applicationCategoryService.UpdateAsync(id, updateReq);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _applicationCategoryService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}