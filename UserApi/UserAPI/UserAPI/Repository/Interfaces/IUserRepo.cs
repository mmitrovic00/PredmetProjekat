using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
	public interface IUserRepo
	{
		List<User> GetUsers();
		User GetUser(string email);
		User GetUser(string email, out UserImage image);
		void AddUser(User user);
		void UpdateUser(User user);

		List<User> GetCustomers();
		List<User> GetSellers();
		void SetSellerStatus(Seller seller);
		void AddUserImage(UserImage image);
		UserImage GetUserImage(string userID);
		void UpdateUserImage(UserImage image);
	}
}
