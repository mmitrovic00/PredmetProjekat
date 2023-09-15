using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.DTO;

namespace WebProjekat.Interfaces
{
	public interface IOrderService
	{
        bool NewOrder(OrderDto order);
        bool CancelOrder(int orderID, string customerID, out string message);
        List<OrderItemDto> GetOrderItems(int orderId);
        List<OrderDto> GetPendingBySeller(string sellerId);
        List<OrderDto> GetOrdersByCustomer(string customerID);
        List<OrderDto> GetOrders();
        List<OrderDto> GetDeliveredBySeller(string sellerID);
    }
}
