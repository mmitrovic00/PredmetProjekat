using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.DTO;
using WebProjekat.Interfaces;
using WebProjekat.Services;

namespace WebProjekat.Controllers
{
	[Route("api/[controller]")]
	[Authorize(Roles = "SELLER")]
	[ApiController]
	public class ItemController : ControllerBase
	{
		private readonly IItemService _itemService;
		public ItemController (IItemService itemService)
		{
			_itemService = itemService;
		}

        [HttpPost("addItem")]
        public IActionResult AddItem([FromBody] ItemDto item)
        {
            string sellerId = User.Identity.Name;
            _itemService.NewItem(item, sellerId);
            return Ok();
        }

        [HttpGet("items/{email}")]
        public IActionResult GetItemsBySeller(string email)
        {
            if (!User.Identity.Name.Equals(email))
                return BadRequest("You can only view your products");

            return Ok(_itemService.ItemsBySeller(email));
        }

        [AllowAnonymous]
        [HttpGet("details/{productID}")]
        public IActionResult GetItem(string itemId)
        {
            var result = Int32.TryParse(itemId, out int id);
            if (!result)
                return BadRequest("Inavlid product ID");

            var product = _itemService.GetItem(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [AllowAnonymous]
        [HttpGet("items")]
        public IActionResult GetItemss()
        {
            return Ok(_itemService.GetItems());
        }

        [HttpPost("modify")]
        public IActionResult ModifyItem([FromBody] ItemDto item)
        {

            var res = _itemService.UpdateItem(item, User.Identity.Name, out string message);

            if (!res)
                return BadRequest(message);
            return Ok(message);
        }

        [HttpDelete("remove/{productID}")]
        public IActionResult RemoveItem(string itemId)
        {
            if (!Int32.TryParse(itemId, out int id))
                return BadRequest("Invalid productID");

            var res = _itemService.DeleteItem(id, User.Identity.Name, out string message);
            if (!res)
                return BadRequest(message);
            return Ok(message);
        }
    }
}
