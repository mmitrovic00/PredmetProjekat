using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjekat.Models
{
	public class ItemImage
	{
		public int ItemImageId { get; set; }
		public int ItemId { get; set; }
		public string ImageTitle { get; set; }
		public byte[] ImageData { get; set; }
	}
}
