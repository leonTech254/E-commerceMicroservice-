using Microsoft.AspNetCore.Mvc;
using CartModel_namespace;
using Cartservice_namespace;

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

		[HttpPost("AddToCart")]
		public IActionResult AddToCart([FromBody] CartModel model)
		{
			var result = _cartService.AddToCart(model);
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
			var result = _cartService.getCartByUserId(userId, id);
			return Ok(result);
		}
	}
}
