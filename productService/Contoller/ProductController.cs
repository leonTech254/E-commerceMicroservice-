using Microsoft.AspNetCore.Mvc;
using ProductModel_namespace;
using ProductService_namespace;
using ProductServiceInterface_namespace;

namespace YourNamespace.Controllers
{
	[Route("api/v1/products/")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ProductService _productService;

		public ProductController(ProductService productService)
		{
			_productService = productService;
		}

		[HttpPost("AddProduct")]
		public IActionResult AddProduct([FromBody] ProductModel product)
		{
			var result = _productService.AddProduct(product);
			return Ok(result);
		}

		[HttpDelete("DeleteProduct")]
		public IActionResult DeleteProduct([FromBody] ProductModel product)
		{
			var result = _productService.DeleteProduct(product);
			return Ok(result);
		}

		[HttpGet("GetAllProducts")]
		public IActionResult GetAllProducts()
		{
			var result = _productService.getAllProducts();
			return Ok(result);
		}

		[HttpPut("UpdateProduct")]
		public IActionResult UpdateProduct([FromBody] ProductModel product)
		{
			var result = _productService.UpdateProduct(product);
			return Ok(result);
		}
	}
}
