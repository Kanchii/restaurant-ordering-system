using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.BO;
using RestaurantOrderingSystem.Models;
using RestaurantOrderingSystem.Models.Entities.Product;
using System.Net;

namespace RestaurantOrderingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBO _productBO;

        public ProductController(IProductBO productBO)
        {
            _productBO = productBO;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var allProducts = _productBO.GetAllProducts();

            return Ok(allProducts);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById([FromRoute] int productId)
        {
            var product = await _productBO.GetProductByIdAsync(productId);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("{productId:int}/category")]
        public IActionResult GetProductWithCategoryById([FromRoute] int productId)
        {
            var product = _productBO.GetProductByIdWithCategory(productId);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest createProductRequest)
        {
            var createProduct = await _productBO.CreateProductAsync(createProductRequest);
            return Ok(createProduct);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductByIdAsync([FromRoute] int productId, [FromBody] UpdateProductRequest updateProductRequest)
        {
            var product = await _productBO.UpdateProductAsync(productId, updateProductRequest);

            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductByIdAsync([FromRoute] int productId)
        {
            var product = await _productBO.DeleteProductAsync(productId);

            return Ok(product);
        }
    }
}
