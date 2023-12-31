using DBconnection_namespace;
using IorderService_namespace;
using OrderModel_namespace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace orderService_namespace
{
	public class OrderService : Iorder
	{
		private readonly DBconn _dBconn;

		public OrderService(DBconn dBconn)
		{
			_dBconn = dBconn;
		}

		public string CreateOrder(OrderModel model)
		{
			_dBconn.orders.Add(model);
			_dBconn.SaveChanges();
			return "Order placed Successfuly";
		}

		public string DeleteOrder(int id)
		{
			var orderToDelete = GetOrder(id);

			if (orderToDelete != null)
			{
				_dBconn.orders.Remove(orderToDelete);
				_dBconn.SaveChanges();
				return "Deleted Successfully";
			}
			else
			{
				return "Order not found";
			}
		}

		public List<OrderModel> getAllOrderByUserID(string userId)
		{
			return _dBconn.orders.Where(o => o.userId == userId).ToList();
		}

		public OrderModel GetOrder(int id)
		{
			return _dBconn.orders.FirstOrDefault(o => o.Id == id);
		}

		public List<OrderModel> GetOrders()
		{
			return _dBconn.orders.ToList();
		}

		/*public string UpdateOrder(OrderModel orderModel)
		{
			var existingOrder = GetOrder(orderModel.Id);

			if (existingOrder != null)
			{
				// Update properties of existingOrder with orderModel
				existingOrder.Property1 = orderModel.;
				existingOrder.Property2 = orderModel.Property2;
				// ... (update other properties as needed)

				_dBconn.SaveChanges();
				return "Updated Successfully";
			}
			else
			{
				return "Order not found";
			}
		}*/
	}
}
