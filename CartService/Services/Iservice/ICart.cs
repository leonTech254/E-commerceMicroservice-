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
		CartModel getCartByUserIdAndCartId(String userId,int id);
		List<CartModel> getAllCartByUserId(String userId);
		Task<String> AddToCart(int id,String token,int quantity);
		String IncreaseQuantity(int id,String token,int quantity);
		String DecreaseQuantity(int id, String token, int quantity);



	}


}