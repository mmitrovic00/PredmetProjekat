using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
	public interface IImageRepo
	{
		void AddItemImage(ItemImage image);
		ItemImage GetItemImage(int itemId);
		void UpdateItemImage(ItemImage image);
	}
}
