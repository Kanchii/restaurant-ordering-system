using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Models;
using RestaurantOrderingSystem.Models.Entities.Category;

namespace RestaurantOrderingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public CategoryController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public IActionResult GetAllCategoriesAsync()
        {
            if (_databaseContext.Categories is null)
                return NotFound();

            var categories = _databaseContext.Categories.Select(x => x);

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCategoryByIdAsync([FromRoute] int id)
        {
            if (_databaseContext.Categories is null)
                return NotFound();

            var category = _databaseContext.Categories.FirstOrDefault(c => c.Id == id);

            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("{id:int}/products")]
        public IActionResult GetCategoryWithProductsById([FromRoute] int id)
        {
            if (_databaseContext.Categories is null)
                return NotFound();

            var category = _databaseContext
                .Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryRequest categoryRequest)
        {
            var category = new Category
            {
                Name = categoryRequest.Name
            };
            var categoryCreated = await _databaseContext.Categories.AddAsync(category);

            await _databaseContext.SaveChangesAsync();

            return Ok(categoryCreated.Entity);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoryByIdAsync([FromRoute] int id)
        {
            var category = await _databaseContext.Categories.FindAsync(id);

            if (category is null)
                return NotFound();

            var deletedCategory = _databaseContext.Categories.Remove(category);

            await _databaseContext.SaveChangesAsync();

            return Ok(deletedCategory.Entity);
        }
    }
}
