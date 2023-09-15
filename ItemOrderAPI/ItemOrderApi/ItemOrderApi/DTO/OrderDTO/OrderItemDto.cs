using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat.DTO
{
	public class OrderItemDto
	{
        public int OrderItemId { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
    }
}
