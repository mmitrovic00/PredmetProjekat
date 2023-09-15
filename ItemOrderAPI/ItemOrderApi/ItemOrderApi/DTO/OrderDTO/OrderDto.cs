using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models.Enum;

namespace WebProjekat.DTO
{
	public class OrderDto
	{
		public int OrderId { get; set; }
		public string CustomerId { get; set; }
		public string Address { get; set; }
		public string Comment { get; set; }
		public double Cost { get; set; }
		public double Time { get; set; }
		public DateTime DateOfDelivery { get; set; }
		public DateTime DateOfOrder { get; set; }
		public EOrder OrderStatus { get; set; }
		public List<OrderItemDto> OrderedItems { get; set; }
	}
}
