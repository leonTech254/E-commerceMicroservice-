using OrderModel_namespace;
using payment_namespace;

namespace IorderService_namespace
{
	public interface Iorder
	{
		Task<String> CreateOrder(int id, String token, PaymentModel payment);
		List<OrderModel> GetOrders();
		OrderModel GetOrder(int id);
		String DeleteOrder(int id);
		/*String UpdateOrder(OrderModel orderModel);*/
		List<OrderModel> getAllOrderByUserID(string userId);

	}
}