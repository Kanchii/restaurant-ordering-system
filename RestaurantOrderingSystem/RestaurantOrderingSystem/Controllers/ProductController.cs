using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystem.Models;
using RestaurantOrderingSystem.Models.Entities.Product;

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
        public IActionResult GetProductById([FromRoute] int productId)
        {
            var product = _databaseContext.Products.Find(productId);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProductAsync([FromBody] CreateProductRequest createProductRequest)
        {
            var product = new Product
            {
                Name = createProductRequest.Name,
                Price = createProductRequest.Price
            };

            _databaseContext.Products.Add(product);
            await _databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductByIdAsync([FromRoute] int productId, [FromBody] UpdateProductRequest updateProductRequest)
        {
            var product = _databaseContext.Products.FirstOrDefault(x => x.Id == productId);
            if (product is null)
                return NotFound();

            product.Name = string.IsNullOrWhiteSpace(updateProductRequest.Name) ? product.Name : updateProductRequest.Name;
            product.Price = updateProductRequest.Price ?? product.Price;

            await _databaseContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductByIdAsync([FromRoute] int productId)
        {
            var product = await _databaseContext.Products.FindAsync(productId);
            
            if (product is null)
                return NotFound();

            _databaseContext.Products.Remove(product);

            await _databaseContext.SaveChangesAsync();

            return Ok();
        }
    }
}
