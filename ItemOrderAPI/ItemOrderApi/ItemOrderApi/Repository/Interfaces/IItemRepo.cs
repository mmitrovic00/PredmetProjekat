using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Repository.Interfaces
{
	public interface IItemRepo
	{
        void AddItem(Item item);
        List<Item> GetItems();
        Item GetItem(int id);
        void UpdateItem(Item item);
        bool DeleteItem(Item item, ItemImage productImage);
        List<int> GetItemsBySeller(string sellerID);
        Item GetDetailedProduct(int id, out ItemImage image);
    }
}
