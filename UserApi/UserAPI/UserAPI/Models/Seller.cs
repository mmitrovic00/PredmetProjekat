using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models.Enum;

namespace WebProjekat.Models
{
	public class Seller : User
	{
		public ESeller Approved { get; set; }
	}
}
