using CartModel_namespace;
using DBconnection_namespace;
using Icart_Namespace;

namespace Cartservice_namespace
{
	public class MycartService : Icart
	{
		private readonly DBconn _dbconn;
		public MycartService(DBconn dbconn)
		{
			_dbconn = dbconn;
		}
		public string AddToCart(CartModel model)
		{
			_dbconn.cart.Add(model);
			_dbconn.SaveChanges();
			return "added to cart successfully";

		}

		public List<CartModel> getAll()
		{
			List<CartModel> cartList = _dbconn.cart.ToList();
			return cartList;
		}

		public List<CartModel> getAllCartByUserId(string userId)
		{
			List<CartModel> AllcartList = _dbconn.cart.ToList();
			List<CartModel> userCartList = AllcartList.Where(e => e.userId == userId).ToList();
			return userCartList;

		}

		public CartModel getById(int id)
		{
			CartModel cart = _dbconn.cart.FirstOrDefault(e => e.Id == id);
			return cart;
		}

		public CartModel getCartByUserId(string userId, int id)
		{
			CartModel cart = _dbconn.cart.FirstOrDefault(e => e.Id == id && e.userId == userId);
			return cart;


		}
	}
}