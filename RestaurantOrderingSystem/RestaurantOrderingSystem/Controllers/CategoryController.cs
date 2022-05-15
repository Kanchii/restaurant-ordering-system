using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.BO;
using RestaurantOrderingSystem.Models;
using RestaurantOrderingSystem.Models.Entities.Category;

namespace RestaurantOrderingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryBO _categoryBO;

        public CategoryController(ICategoryBO categoryBO)
        {
            _categoryBO = categoryBO;
        }

        [HttpGet]
        public IActionResult GetAllCategoriesAsync()
        {
            var categories = _categoryBO.GetAllCategories();

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] int id)
        {
            var category = await _categoryBO.GetCategoryAsync(id);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("{id:int}/products")]
        public IActionResult GetCategoryWithProductsById([FromRoute] int id)
        {
            var category = _categoryBO.GetCategoryWithProducts(id);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequest categoryRequest)
        {
            var category = await _categoryBO.CreateCategoryAsync(categoryRequest);

            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] int id, [FromBody] UpdateCategoryRequest updateCategoryRequest)
        {
            var updatedCategory = await _categoryBO.UpdateCategoryAsync(id, updateCategoryRequest);
            if (updatedCategory is null)
                return NotFound();

            return Ok(updatedCategory);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoryByIdAsync([FromRoute] int id)
        {
            var deletedCategory = await _categoryBO.DeleteCategory(id);
            if (deletedCategory is null)
                return NotFound();

            return Ok(deletedCategory);
        }
    }
}
