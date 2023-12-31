using CartModel_namespace;
using DBconnection_namespace;
using ExternalApiData_namespace;
using IorderService_namespace;
using JwTNameService;
using OrderModel_namespace;
using payment_namespace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace orderService_namespace
{
	public class OrderService : Iorder
	{
		private readonly DBconn _dBconn;
		private readonly Jwt _jwt;
		private readonly ExternalAPI _externalAPI;


		public OrderService(DBconn dBconn,Jwt jwt,ExternalAPI externalAPI)
		{
			_dBconn = dBconn;
			_jwt = jwt;
			_externalAPI = externalAPI;
		}

		public async Task<String> CreateOrder(int id,String token,PaymentModel payment)
		{
			CartModel cart=await _externalAPI.GetCartFromCartService(id, token);
			Console.WriteLine("Hello leon");

			String userId = _jwt.GetUserIdFromToken(token);
			Console.WriteLine("Hello leon2");


			if (cart!=null)
			{
				//check if the use have paid the full amount
				if(cart.totalprice>payment.AmountInKsh)
				{
					return "Order Failed to place,Money should be paid in Full";
				}else
				{
					OrderModel order = new OrderModel()
					{
						userId = userId,
						payAmount=payment.AmountInKsh,
						productId=cart.productId,
						datePlaced=DateTime.Now,
						quantity=cart.quantity
					};
					_dBconn.orders.Add(order);
					_dBconn.SaveChanges();
					return "Order placed Successfuly";

				}
			}
			else
			{
				return "Cart not found";

			}




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
