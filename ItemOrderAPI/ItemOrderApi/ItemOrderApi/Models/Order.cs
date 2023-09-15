using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models.Enum;

namespace WebProjekat.Models
{
	public class Order
	{
		public int OrderId { get; set; }
		public string CustomerId { get; set; }
		public int Amount { get; set; }
		public string Address { get; set; }
		public string Comment { get; set; }
		public double Cost { get; set; }
		public double Time { get; set; }
		public DateTime DateofDelivery { get; set; }
		public DateTime DateofOrder { get; set; }
		public EOrder OrderStatus { get; set; }
		public List<OrderItem> OrderedItems { get; set; }
	}
}
