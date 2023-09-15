using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;
using WebProjekat.Models.Enum;

namespace WebProjekat.Repository.Interfaces
{
	public interface IOrderRepo
	{
        bool NewOrder(Order order, List<Item> items);
        Order GetOrderById(int orderId);
        List<OrderItem> GetOrderItems(int orderId);
        bool CancelOrder(Order order, List<Item> items);
        bool UpdateOrder(Order order);
        List<Order> GetByCustomer(string customerId);
        List<Order> GetOrders();
        List<int> GetOrdersByItemsIDs(List<int> productIDs);
        List<Order> GetOrdersBySeller(List<int> orderIds, EOrder orderStatus);
        List<OrderItem> GetOrderItemsBySeller(int orderId, List<int> productsIds);
        
    }
}
