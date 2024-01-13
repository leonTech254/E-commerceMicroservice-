using Microsoft.AspNetCore.Mvc;
using CartModel_namespace;
using Cartservice_namespace;
using Microsoft.AspNetCore.Authorization;

namespace CartController_namespace
{
	[Route("api/cart/")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly MycartService _cartService;

		public CartController(MycartService cartService)
		{
			_cartService = cartService;
		}
		// [Authorize]
		[HttpPost("AddToCart/{productId}/{quantity}/")]
		public async Task<IActionResult> AddToCartAsync(int productId, int quantity)
		{
			string jwtToken = HttpContext.Request.Headers["Authorization"];


			var result = await _cartService.AddToCart(productId, jwtToken, quantity);
			return Ok(result);
		}

		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _cartService.getAll();
			return Ok(result);
		}

		[HttpGet("GetAllCartByUserId/{userId}")]
		public IActionResult GetAllCartByUserId(string userId)
		{
			var result = _cartService.getAllCartByUserId(userId);
			return Ok(result);
		}

		[HttpGet("GetById/{id}")]
		public IActionResult GetById(int id)
		{
			var result = _cartService.getById(id);
			return Ok(result);
		}

		[HttpGet("GetCartByUserId/{userId}/{id}")]
		public IActionResult GetCartByUserId(string userId, int id)
		{
			var result = _cartService.getCartByUserIdAndCartId(userId, id);
			return Ok(result);
		}

		[HttpPost("increase-quantity")]
		[Authorize]
		public IActionResult IncreaseQuantity([FromForm] int id, [FromForm] int quantity)
		{
			string token = HttpContext.Request.Headers["Authorization"]; // Assuming token is passed in the header
			string result = _cartService.IncreaseQuantity(id, token, quantity);

			if (result.Contains("successfully"))
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("decrease-quantity")]
		[Authorize] // Add authorization as needed
		public IActionResult DecreaseQuantity([FromForm] int id, [FromForm] int quantity)
		{
			string token = HttpContext.Request.Headers["Authorization"]; // Assuming token is passed in the header
			string result = _cartService.DecreaseQuantity(id, token, quantity);

			if (result.Contains("successfully"))
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}
	}
}