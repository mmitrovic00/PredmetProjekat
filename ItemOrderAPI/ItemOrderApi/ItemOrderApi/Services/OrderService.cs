using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.DTO;
using WebProjekat.Interfaces;
using WebProjekat.Models;
using WebProjekat.Models.Enum;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Services
{
	public class OrderService : IOrderService
	{
		private readonly IMapper _mapper;
		private readonly IConfigurationSection _secretKey;
		private readonly IOrderRepo _orderRepository;
		private readonly IItemRepo _itemRepository;

		public OrderService(IMapper mapper, IOrderRepo orderRepository,IItemRepo itemRepository, IConfiguration config)
		{
			_mapper = mapper;
			_secretKey = config.GetSection("SecretKey");
			_orderRepository = orderRepository;
			_itemRepository = itemRepository;
		}

		public bool CancelOrder(int orderId, string customerID, out string message)
		{
			var order = _orderRepository.GetOrderById(orderId);
			if (order == null)
			{
				message = "Order doesn't exist";
				return false;
			}

			DateTime futureDateTime = order.DateofOrder.AddHours(1);
			//DateTime futureDateTime = order.DateofOrder.AddSeconds(5); 

			if (DateTime.Now > futureDateTime)
			{

				message = "time to cancel has expired ";
				return false;
			}

			order.OrderStatus = EOrder.CANCELED;

			
			var orderedItems = _orderRepository.GetOrderItems(orderId);
			List<Item> items = new();

			foreach (var orderItem in orderedItems)
			{
				var item = _itemRepository.GetItem(orderItem.ItemId);
				if (item != null)
				{
					item.Amount += orderItem.Amount;
					items.Add(item);
				}
			}

			var response = _orderRepository.CancelOrder(order, items);

			if (response)
			{
				message = "success";
				return true;
			}
			message = "Fail";
			return false;
		}

		public bool UpdateOrder(OrderDto order)
		{
			return true;
		}
		public List<OrderDto> GetOrders()
		{
			var orders = _orderRepository.GetOrders();
			List<OrderDto> retOrders = new();

			foreach (var order in orders)
			{
				if (order.DateofDelivery < DateTime.Now)
					order.OrderStatus = EOrder.DELIVERED;
				var mapped = _mapper.Map<OrderDto>(order);
				mapped.OrderedItems = _mapper.Map<List<OrderItemDto>>(_orderRepository.GetOrderItems(order.OrderId));
				retOrders.Add(mapped);
				_orderRepository.UpdateOrder(order);
			}
			
			return retOrders;
		}


		public List<OrderDto> GetOrdersByCustomer(string customerID)
		{
			var orders = _orderRepository.GetByCustomer(customerID);
			List<OrderDto> retOrders = new();

			foreach (var order in orders)
			{
				if (order.DateofDelivery < DateTime.Now)
					order.OrderStatus = EOrder.DELIVERED;
				var mapped = _mapper.Map<OrderDto>(order);
				mapped.OrderedItems = _mapper.Map<List<OrderItemDto>>(_orderRepository.GetOrderItems(order.OrderId));
				retOrders.Add(mapped);
			}

			return retOrders;
		}

		public bool NewOrder(OrderDto order)
		{
			Item item;
			List<Item> items = new();
			foreach (var orderItem in order.OrderedItems)
			{
				item = _itemRepository.GetItem(orderItem.ItemId);
				if (item == null)
					return false;
				item.Amount -= orderItem.Amount;
				_itemRepository.UpdateItem(item); 

				
				items.Add(item);
			}
			Order newOrder = _mapper.Map<Order>(order);
			List<OrderItem> orderItems = _mapper.Map<List<OrderItem>>(order.OrderedItems);
			newOrder.OrderedItems = orderItems;
			Random random = new Random();

			DateTime now = DateTime.Now;

			// Generate a random number of minutes between 60 and 1439 (1 to 23 hours)
			int randomMinutes = random.Next(60, 1440);

			// Calculate the random time at least 1 hour after now
			DateTime randomDateTime = now.AddMinutes(randomMinutes);

			// Ensure the random time is not more than 14 days in the future
			DateTime maxDateTime = now.AddDays(14);
			randomDateTime = randomDateTime > maxDateTime ? maxDateTime : randomDateTime;

			newOrder.OrderStatus = EOrder.IN_PROGRESS;
			newOrder.DateofDelivery = randomDateTime;
			newOrder.DateofOrder = DateTime.Now;
			_orderRepository.NewOrder(newOrder, items);
			
			return true;
		}

		public List<OrderItemDto> GetOrderItems(int orderId)
		{
			var orderItems = _mapper.Map<List<OrderItem>>(_orderRepository.GetOrderItems(orderId));

			return _mapper.Map<List<OrderItemDto>>(orderItems);
		}

		public List<OrderDto> GetDeliveredBySeller(string sellerId)
		{
			
			List<int> items = _itemRepository.GetItemsBySeller(sellerId); //lista id stvari koji prodaje bas taj prodavac
			var ordersId = _orderRepository.GetOrdersByItemsIDs(items); // lista idejeva od ordera 
			List<OrderDto> orders = new();
			foreach(var orderId in ordersId)
			{ 
				var order = _orderRepository.GetOrderById(orderId);
				if (order.DateofDelivery >= DateTime.Now)
					order.OrderStatus = EOrder.DELIVERED;
				if (order.OrderStatus == EOrder.DELIVERED)
				{
					var mapped = _mapper.Map<OrderDto>(order);
					mapped.OrderedItems = _mapper.Map<List<OrderItemDto>>(_orderRepository.GetOrderItems(order.OrderId));
					orders.Add(mapped);
				}
			}
			return orders;
		}

		public List<OrderDto> GetPendingBySeller(string sellerId)
		{

			List<int> items = _itemRepository.GetItemsBySeller(sellerId); //lista id stvari koji prodaje bas taj prodavac
			var ordersId = _orderRepository.GetOrdersByItemsIDs(items); // lista idejeva od ordera 
			List<OrderDto> orders = new();
			foreach (var orderId in ordersId)
			{
				var order = _orderRepository.GetOrderById(orderId);
				if (order.DateofDelivery >= DateTime.Now)
					order.OrderStatus = EOrder.DELIVERED;
				if (order.OrderStatus == EOrder.IN_PROGRESS)
				{
					var mapped = _mapper.Map<OrderDto>(order);
					mapped.OrderedItems = _mapper.Map<List<OrderItemDto>>(_orderRepository.GetOrderItems(order.OrderId));
					orders.Add(mapped);
				}
			}
			return orders;
		}
	
	}
}
