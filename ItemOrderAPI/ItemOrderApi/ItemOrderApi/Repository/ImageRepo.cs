using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Infrastructure;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Repository
{
	public class ImageRepo : IImageRepo
	{

		private readonly WSDBContext _dbContext;
		private readonly object lockObject = new object();
		public ImageRepo(WSDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void AddItemImage(ItemImage image)
		{
			_dbContext.ItemImages.Add(image);
			_dbContext.SaveChanges();
		}

		public ItemImage GetItemImage(int itemId)
		{
			return _dbContext.ItemImages.Where(x => x.ItemId == itemId).FirstOrDefault();
		}

		public void UpdateItemImage(ItemImage image)
		{
			lock (lockObject)
			{
				_dbContext.ItemImages.Update(image);
				_dbContext.SaveChanges();
			}
		}
	}
}
