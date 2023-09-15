using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.DTO;
using WebProjekat.Interfaces;

namespace WebProjekat.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{

		private readonly IOrderService _orderService;
		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost("newOrder")]
		[Authorize(Roles = "CUSTOMER")]
		public IActionResult CreateOrder(OrderDto order)
		{
			order.CustomerId = User.Identity.Name;
			if (_orderService.NewOrder(order))
				return Ok("Order is placed. You have one hour to cancle it.");
			return BadRequest("");
		}

		[HttpGet("ordersByCustomer")]
		[Authorize(Roles = "CUSTOMER")]
		public IActionResult GetPending()
		{
			return Ok(_orderService.GetOrdersByCustomer(User.Identity.Name));
		}

		[HttpGet("orderedItems")]
		[Authorize(Roles = "ADMIN, CUSTOMER")]
		public IActionResult GetOrderItems(int orderId)
		{
			return Ok(_orderService.GetOrderItems(orderId));
		}

		[HttpDelete("{orderId}")]
		[Authorize(Roles = "ADMIN, CUSTOMER")]
		public IActionResult CancelOrder(string orderId)
		{
			if (!Int32.TryParse(orderId, out int id))
				return BadRequest("Invalid orderID");
			if (!_orderService.CancelOrder(id, User.Identity.Name, out string message))
				return BadRequest(message);
			return Ok(message);
		}

		[HttpGet("orders")]
		[Authorize(Roles = "ADMIN")]
		public IActionResult GetOrders()
		{
			return Ok(_orderService.GetOrders());
		}

		[HttpGet("deliveredOrders")]
		[Authorize(Roles = "SELLER")]
		public IActionResult GetDelivered(string sellerId)
		{
			return Ok(_orderService.GetDeliveredBySeller(sellerId));
		}

		[HttpGet("pendingOrders")]
		[Authorize(Roles = "SELLER")]
		public IActionResult GetPending(string sellerId)
		{
			return Ok(_orderService.GetPendingBySeller(sellerId));
		}
	}
}
