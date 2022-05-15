using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Models;
using RestaurantOrderingSystem.Models.Entities.Category;

namespace RestaurantOrderingSystem.BO
{
    public interface ICategoryBO
    {
        Task<Category?> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest);
        Task<Category?> DeleteCategory(int id);
        IEnumerable<Category> GetAllCategories();
        Task<Category?> GetCategoryAsync(int id);
        Category? GetCategoryWithProducts(int id);
        Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryRequest updateCategoryRequest);
    }

    public class CategoryBO : ICategoryBO
    {
        private readonly DatabaseContext _databaseContext;
        public CategoryBO(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = _databaseContext.Categories;

            return categories;
        }

        public async Task<Category?> CreateCategoryAsync(CreateCategoryRequest createCategoryRequest)
        {
            if (string.IsNullOrWhiteSpace(createCategoryRequest.Name))
                throw new Exception("Category name cannot be null");

            var category = new Category
            {
                Name = createCategoryRequest.Name
            };

            var createdCategory = _databaseContext.Categories.Add(category);

            await _databaseContext.SaveChangesAsync();

            return createdCategory.Entity;
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {
            var category = await _databaseContext.Categories.FindAsync(id);

            return category;
        }

        public Category? GetCategoryWithProducts(int id)
        {
            var category = _databaseContext.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryRequest updateCategoryRequest)
        {
            var category = await GetCategoryAsync(id);
            if (category is null)
                return null;

            category.Name = updateCategoryRequest.Name ?? category.Name;
            await _databaseContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteCategory(int id)
        {
            var category = await GetCategoryAsync(id);
            if (category is null)
                return null;

            var deletedCategory = _databaseContext.Categories.Remove(category);
            await _databaseContext.SaveChangesAsync();

            return deletedCategory.Entity;
        }
    }
}
