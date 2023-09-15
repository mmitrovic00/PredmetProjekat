using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Interfaces
{
	public interface IImageService
	{
		bool AddItemImage(int itemId, string sellerID, IFormFile file);
		ItemImage GetItemImage(int itemId);
		bool UpdateItemImage(IFormFile file, string sellerId, int itemId);
	}
}
