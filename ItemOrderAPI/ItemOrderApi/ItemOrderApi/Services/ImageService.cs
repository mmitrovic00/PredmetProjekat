using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Interfaces;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Services
{
	public class ImageService : IImageService
	{
		private readonly IImageRepo _imageRepository;
		private readonly IItemRepo _itemRepository;

		public ImageService(IImageRepo imageRepository, IItemRepo productRepository)
		{
			_imageRepository = imageRepository;
			_itemRepository = productRepository;
		}
		public bool AddItemImage(int itemId, string sellerID, IFormFile file)
		{
			var product = _itemRepository.GetItem(itemId);
			if (product == null)
				return false;

			if (!product.SellerId.Equals(sellerID))
				return false;

			ItemImage image = new ItemImage()
			{
				ImageTitle = file.FileName,
				ItemId = itemId
			};

			MemoryStream ms = new MemoryStream();
			file.CopyTo(ms);
			image.ImageData = ms.ToArray();

			ms.Close();
			ms.Dispose();

			_imageRepository.AddItemImage(image);
			return true;
		}

		public ItemImage GetItemImage(int itemId)
		{
			return _imageRepository.GetItemImage(itemId);
		}

		public bool UpdateItemImage(IFormFile file, string sellerId, int itemId)
		{
			var product = _itemRepository.GetItem(itemId);
			if (product == null) return false;
			if (product.SellerId != sellerId) return false;

			var image = _imageRepository.GetItemImage(itemId);
			if (image == null)
			{
				image = new ItemImage();
				image.ItemId = itemId;
			}

			MemoryStream ms = new MemoryStream();
			file.CopyTo(ms);
			image.ImageData = ms.ToArray();

			ms.Close();
			ms.Dispose();

			_imageRepository.UpdateItemImage(image);
			return true;
		}
	}
}
