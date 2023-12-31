using OrderModel_namespace;

namespace IorderService_namespace
{
	public interface Iorder
	{
		String CreateOrder(OrderModel model);
		List<OrderModel> GetOrders();
		OrderModel GetOrder(int id);
		String DeleteOrder(int id);
		/*String UpdateOrder(OrderModel orderModel);*/
		List<OrderModel> getAllOrderByUserID(string userId);

	}
}