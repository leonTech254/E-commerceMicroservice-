using Microsoft.AspNetCore.Mvc;
using orderService_namespace;
using OrderModel_namespace;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using payment_namespace;

namespace orderController_namespace
{
	[Route("api/v1/products/")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly OrderService _orderService;

		public OrderController(OrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet("GetOrders")]
		public IActionResult GetOrders()
		{
			var result = _orderService.GetOrders();
			return Ok(result);
		}

		[HttpGet("GetOrder/{id}")]
		public IActionResult GetOrder(int id)
		{
			var result = _orderService.GetOrder(id);

			if (result != null)
			{
				return Ok(result);
			}
			else
			{
				return NotFound("Order not found");
			}
		}

		[HttpGet("GetAllOrderByUserID/{userId}")]
		public IActionResult GetAllOrderByUserID(string userId)
		{
			var result = _orderService.getAllOrderByUserID(userId);
			return Ok(result);
		}

		[HttpPost("CreateOrder/cart/{id}")]
		[Authorize]
		public async Task<IActionResult> CreateOrderAsync(int id,PaymentModel payment)
		{
			string jwtToken = HttpContext.Request.Headers["Authorization"];
			try
			{
				// You might want to add additional validation logic here before creating the order
				var result = await _orderService.CreateOrder(id, jwtToken, payment);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest($"Failed to create order: {ex.Message}");
			}
		}

		[HttpPost("DeleteOrder/{id}")]
		public IActionResult DeleteOrder(int id)
		{
			var result = _orderService.DeleteOrder(id);
			return Ok(result);
		}

		/*[HttpPost("UpdateOrder")]
		public IActionResult UpdateOrder([FromBody] OrderModel orderModel)
		{
			var result = _orderService.UpdateOrder(orderModel);

			if (result == "Updated Successfully")
			{
				return Ok(result);
			}
			else
			{
				return NotFound(result);
			}
		}*/
	}
}
