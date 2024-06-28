using Microsoft.AspNetCore.Mvc;
using PRM392_ShopClothes_Model.Model.Request;
using PRM392_ShopClothes_Service.Interface;

namespace PRM392_ShopClothes_API.Controllers.Product
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string? productName, [FromQuery] int? providerId, [FromQuery] double? unitPrice, [FromQuery] double? weight, [FromQuery] int? categoryId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var (products, totalCount) = await _productService.SearchProductsAsync(productName, providerId, unitPrice, weight, categoryId, pageIndex, pageSize);
            return Ok(new { Products = products, TotalCount = totalCount });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductRequest productRequest) // Changed to [FromForm]
        {
            var createdProduct = await _productService.CreateProductAsync(productRequest);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductRequest productRequest) // Changed to [FromForm]
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, productRequest);
            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
