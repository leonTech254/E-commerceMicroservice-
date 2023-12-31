using CartModel_namespace;

namespace Icart_Namespace
{
	public interface Icart
	{
		void AddService()
		{

		}
		List<CartModel> getAll();
		CartModel getById(int id);
		CartModel getCartByUserId(String userId,int id);
		List<CartModel> getAllCartByUserId(String userId);
		Task<String> AddToCart(int id,String token,int quantity);


	}


}