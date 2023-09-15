using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Infrastructure;
using WebProjekat.Models;
using WebProjekat.Models.Enum;
using WebProjekat.Repository.Interfaces;

namespace WebProjekat.Repository
{
	public class OrderRepo : IOrderRepo
	{
		private readonly WSDBContext _dbContext;

		private readonly object lockObject = new object();


		public OrderRepo(WSDBContext dbContext)
		{
			_dbContext = dbContext;
		}
		public bool CancelOrder(Order order, List<Item> items)
		{

			lock (lockObject)
			{
				_dbContext.Items.UpdateRange(items);
				_dbContext.SaveChanges();

				_dbContext.Orders.Update(order);
				_dbContext.SaveChanges();

				return true;
			}
		}
		
		public bool UpdateOrder(Order order)
		{

			lock (lockObject)
			{

				_dbContext.Orders.Update(order);
				_dbContext.SaveChanges();

				return true;
			}
		}


		public List<Order> GetByCustomer(string customerId)
		{
			return _dbContext.Orders.Where(x => x.CustomerId.Equals(customerId)).ToList();
		}

		public Order GetOrderById(int orderId)
		{
			return _dbContext.Orders.Where(x => x.OrderId.Equals(orderId)).FirstOrDefault();
		}

		public List<OrderItem> GetOrderItems(int orderId)
		{
			return _dbContext.OrderItems.Where(x => x.OrderId == orderId).ToList();
		}

		public List<int> GetOrdersByItemsIDs(List<int> itemIds)
		{
			return _dbContext.OrderItems.Where(x => itemIds.Contains(x.ItemId))
									   .Select(x => x.OrderId)
									   .ToList();
		}

		public List<OrderItem> GetOrderItemsBySeller(int orderId, List<int> itemsIds)
		{
			return _dbContext.OrderItems.Where(x => (x.OrderId == orderId) &&
														(itemsIds.Contains(x.ItemId)))
										   .ToList();
		}

		public List<Order> GetOrders()
		{
			return _dbContext.Orders.ToList();
		}

		public List<Order> GetOrdersBySeller(List<int> orderIds, EOrder orderStatus)
		{
			return _dbContext.Orders.Where(x => (orderIds.Contains(x.OrderId)) && (x.OrderStatus == orderStatus)).ToList();
		}

		public bool NewOrder(Order order, List<Item> items)
		{
			lock (lockObject)
			{
				_dbContext.Items.UpdateRange(items);
				_dbContext.SaveChanges();

				_dbContext.Orders.Add(order);
				_dbContext.SaveChanges();

				return true;
			}
		}

		
	}
}
