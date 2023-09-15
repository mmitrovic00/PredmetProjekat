using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.DTO;

namespace WebProjekat.Interfaces
{
	public interface IItemService
	{
		void NewItem(ItemDto item, string sellerId);
		List<ItemDto> ItemsBySeller(string sellerId);
		List<ItemDto> GetItems();
		bool UpdateItem(ItemDto item, string sellerId, out string message);
		bool DeleteItem(int itemId, string sellerId, out string message);
		ItemDto GetItem(int id);
	}
}
