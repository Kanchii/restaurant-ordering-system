using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Models;
using RestaurantOrderingSystem.Models.Entities.Product;
using System.Net;

namespace RestaurantOrderingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public ProductController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var allProducts = _databaseContext.Products.Select(x => x);

            return Ok(allProducts);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById([FromRoute] int productId)
        {
            var product = await _databaseContext.Products.FindAsync(productId);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest createProductRequest)
        {
            var category = await _databaseContext.Categories.FindAsync(createProductRequest.CategoryId);
            if (category is null)
                return BadRequest($"Category ID {createProductRequest.CategoryId} not exists");

            var product = new Product
            {
                Name = createProductRequest.Name,
                Price = createProductRequest.Price,
                CategoryId = createProductRequest.CategoryId
            };

            var createdProduct = _databaseContext.Products.Add(product);

            await _databaseContext.SaveChangesAsync();

            return Ok(createdProduct.Entity);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductByIdAsync([FromRoute] int productId, [FromBody] UpdateProductRequest updateProductRequest)
        {
            var product = await _databaseContext.Products.FindAsync(productId);
            if (product is null)
                return NotFound();

            if(updateProductRequest.CategoryId is not null)
            {
                var category = await _databaseContext.Categories.FindAsync(updateProductRequest.CategoryId);
                if (category is null)
                    return BadRequest($"Category ID {updateProductRequest.CategoryId} not exists");
            }

            product.Name = string.IsNullOrWhiteSpace(updateProductRequest.Name) ? product.Name : updateProductRequest.Name;
            product.Price = updateProductRequest.Price ?? product.Price;
            product.CategoryId = updateProductRequest.CategoryId ?? product.CategoryId;

            await _databaseContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductByIdAsync([FromRoute] int productId)
        {
            var product = await _databaseContext.Products.FindAsync(productId);
            
            if (product is null)
                return NotFound();

            var deletedProduct = _databaseContext.Products.Remove(product);

            await _databaseContext.SaveChangesAsync();

            return Ok(deletedProduct.Entity);
        }
    }
}
