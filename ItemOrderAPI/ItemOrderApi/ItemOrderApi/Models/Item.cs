using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat.Models
{
	public class Item
	{
		public int ItemId { get; set; }
		public string ItemName { get; set; }
		public double Price { get; set; }
		public int Amount { get; set; }
		public string Description { get; set; }
		public string ImagePath { get; set; }
		public string SellerId { get; set; }
		public List<OrderItem> OrderItems { get; set; }
	}
}
