using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models.Enum;

namespace WebProjekat.DTO.User
{
	public class TokenDTO
	{
		public string Token { get; set; }
		public EUserType UserType { get; set; }
	}
}
