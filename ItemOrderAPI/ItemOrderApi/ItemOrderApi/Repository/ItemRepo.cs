using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Infrastructure;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Repository
{
	public class ItemRepo : IItemRepo
	{
		private readonly WSDBContext _dbContext;

		private readonly object lockObject = new object();


		public ItemRepo(WSDBContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void AddItem(Item item)
		{
			_dbContext.Items.Add(item);
			_dbContext.SaveChanges();
		}

		public bool DeleteItem(Item item, ItemImage productImage)
		{
			using var transaction = _dbContext.Database.BeginTransaction();
			try
			{
				lock (lockObject)
				{
					_dbContext.Items.Remove(item);
					_dbContext.SaveChanges();

					if (productImage != null)
					{
						_dbContext.ItemImages.Remove(productImage);
						_dbContext.SaveChanges();
					}

					transaction.Commit();
					return true;
				}
			}
			catch (Exception e)
			{
				transaction.Rollback();
				return false;
			}
		}
		public Item GetDetailedProduct(int id, out ItemImage image)
		{
			using var transaction = _dbContext.Database.BeginTransaction();
			try
			{
				lock (lockObject)
				{
					var product = _dbContext.Items.Find(id);
					image = _dbContext.ItemImages.Where(x => x.ItemId.Equals(product.ItemId)).FirstOrDefault();

					transaction.Commit();
					return product;
				}
			}
			catch (Exception e)
			{
				transaction.Rollback();
				image = null;
				return null;
			}
		}
		public Item GetItem(int id)
		{
			return _dbContext.Items.Where(x => x.ItemId.Equals(id)).FirstOrDefault();
		}

		public List<Item> GetItems()
		{
			return _dbContext.Items.ToList();
		}

		public List<int> GetItemsBySeller(string sellerID)
		{
			return _dbContext.Items.Where(x => x.SellerId.Equals(sellerID))
									.Select(x => x.ItemId)
									.ToList();
		}

		public void UpdateItem(Item item)
		{
			lock (lockObject)
			{
				_dbContext.Items.Update(item);
				_dbContext.SaveChanges();
			}
		}
	}
}
