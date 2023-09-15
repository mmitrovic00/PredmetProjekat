using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models.Enum;

namespace WebProjekat.DTO.User
{
	public class UserDto
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Birthday { get; set; }
		public string Address { get; set; }
		public string ImagePath { get; set; }

		public ESeller Approved { get; set; }
	}
}
