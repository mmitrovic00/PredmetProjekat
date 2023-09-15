using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.DTO.User;
using WebProjekat.Models;
using WebProjekat.Models.Enum;

namespace WebProjekat.Interfaces
{
	public interface IUserService
	{
		UserDto GetByEmail(string email);
		bool UpdateUser(UpdateUserDto newUser);
		bool ChangePassword(PasswordDto data, out string mess);
		TokenDTO Registration(RegistrationUserDto dto, out string mess);
		TokenDTO LogIn(LogInDto dto);
		TokenDTO GoogleLogInUser(GoogleLogInDto newUser);
		List<UserDto> GetCustomers();
		List<UserDto> GetSellers();
		bool SetSellerStatus(string email, ESeller status);
		void AddUserImage(string userID, IFormFile file);
		UserImage GetUserImage(string userID);
		bool UpdateUserImage(IFormFile file, string userID);
	}
}
