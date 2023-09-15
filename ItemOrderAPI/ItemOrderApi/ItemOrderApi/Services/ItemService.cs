using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.DTO;
using WebProjekat.Interfaces;
using WebProjekat.Models;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Services
{
	public class ItemService : IItemService
	{
		private readonly IMapper _mapper;
		private readonly IConfigurationSection _secretKey;
		private readonly IItemRepo _itemRepository;
		private readonly IImageRepo _imageRepository;
		public ItemService(IMapper mapper, IItemRepo itemRepository, IConfiguration config, IImageRepo imageRepo)
		{
			_mapper = mapper;
			_secretKey = config.GetSection("SecretKey");
			_itemRepository = itemRepository;
			_imageRepository = imageRepo;
		}
		public bool DeleteItem(int itemId, string sellerId, out string message)
		{
			Item item = _itemRepository.GetItem(itemId);
			ItemImage itemImage = _imageRepository.GetItemImage(itemId);
			if (item == null)
			{
				message = "";
				return false;
			}

			if (!item.SellerId.Equals(sellerId))
			{
				message = "";
				return false;
			}

			_itemRepository.DeleteItem(item, itemImage);
			message = "Uspesno obrisan proizvod";

			return true;
		}

		public ItemDto GetItem(int id)
		{
			var item = _itemRepository.GetDetailedProduct(id, out	ItemImage image);
			if (item == null)
				return null;
			var result = _mapper.Map<ItemDto>(item);
			if (image != null)
			{
				string imageBase64Data = Convert.ToBase64String(image.ImageData);
				result.Image = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
			}
			else
				result.Image = "";
			return result;
		}

		public List<ItemDto> GetItems()
		{
			var items = _itemRepository.GetItems();
			List<ItemDto> detailItems = new List<ItemDto>();
			foreach(var item in items)
			{
				var detailItem = _itemRepository.GetDetailedProduct(item.ItemId, out ItemImage image);
				if (detailItem == null)
					return null;
				var result = _mapper.Map<ItemDto>(detailItem);
				if (image != null)
				{
					string imageBase64Data = Convert.ToBase64String(image.ImageData);
					result.Image = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
				}
				else
					result.Image = "";
				detailItems.Add(result);
			}
			return _mapper.Map<List<ItemDto>>(detailItems);
		}

		public List<ItemDto> ItemsBySeller(string sellerId)
		{
			var items = _itemRepository.GetItems();
			return _mapper.Map<List<ItemDto>>(items.Where(x => x.SellerId.Equals(sellerId)));
		}

		public void NewItem(ItemDto item, string sellerId)
		{
			Item newItem = _mapper.Map<Item>(item);
			newItem.SellerId = sellerId;
			_itemRepository.AddItem(newItem);
		}

		public bool UpdateItem(ItemDto newItem, string sellerId, out string message)
		{
			Item item = _itemRepository.GetItem(newItem.ItemId);
			if(item == null)
			{
				message = "";
				return false;
			}

			if (!item.SellerId.Equals(sellerId))
			{
				message = "";
				return false;
			}

			item.ItemName = newItem.ItemName;
			item.Amount = newItem.Amount;
			item.Price = newItem.Price;
			item.Description = newItem.Description;

			_itemRepository.UpdateItem(item);

			message = "Uspesno ste azurirali proizvod.";
			return true;
		}
	}
}
