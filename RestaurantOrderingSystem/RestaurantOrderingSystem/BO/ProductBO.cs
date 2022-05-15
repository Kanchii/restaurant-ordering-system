using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Models;
using RestaurantOrderingSystem.Models.Entities.Product;

namespace RestaurantOrderingSystem.BO
{
    public interface IProductBO
    {
        Task<Product> CreateProductAsync(CreateProductRequest createProductRequest);
        Task<Product?> DeleteProductAsync(int id);
        IEnumerable<Product> GetAllProducts();
        Task<Product?> GetProductByIdAsync(int id);
        Product? GetProductByIdWithCategory(int id);
        Task<Product?> UpdateProductAsync(int id, UpdateProductRequest updateProductRequest);
    }

    public class ProductBO : IProductBO
    {
        private readonly DatabaseContext _databaseContext;
        public ProductBO(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _databaseContext.Products;

            return products;
        }

        public async Task<Product> CreateProductAsync(CreateProductRequest createProductRequest)
        {
            var category = await _databaseContext.Categories.FindAsync(createProductRequest.CategoryId);
            if (category is null)
                throw new Exception($"Category ID {createProductRequest.CategoryId} not exists");

            var product = new Product
            {
                Name = createProductRequest.Name,
                Price = createProductRequest.Price,
                CategoryId = createProductRequest.CategoryId
            };

            var createdProduct = _databaseContext.Products.Add(product);

            await _databaseContext.SaveChangesAsync();

            return createdProduct.Entity;
        }
    
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _databaseContext.Products.FindAsync(id);

            await _databaseContext.SaveChangesAsync();

            return product;
        }

        public Product? GetProductByIdWithCategory(int id)
        {
            var product = _databaseContext.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id, UpdateProductRequest updateProductRequest)
        {
            var product = await GetProductByIdAsync(id);
            if (product is null)
                return null;

            if (updateProductRequest.CategoryId is not null)
            {
                var category = await _databaseContext.Categories.FindAsync(updateProductRequest.CategoryId);
                if (category is null)
                    throw new Exception($"Category ID {updateProductRequest.CategoryId} not exists");
            }

            product.Name = updateProductRequest.Name ?? product.Name;
            product.Price = updateProductRequest.Price ?? product.Price;
            product.CategoryId = updateProductRequest.CategoryId ?? product.CategoryId;

            await _databaseContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product is null)
                return null;

            var removedProduct = _databaseContext.Products.Remove(product);
            await _databaseContext.SaveChangesAsync();

            return removedProduct.Entity;
        }
    }
}
