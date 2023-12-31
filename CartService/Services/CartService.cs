using CartModel_namespace;
using DBconnection_namespace;
using ExternalApiData_namespace;
using Icart_Namespace;
using JwTNameService;
using ProductModel_namespace;

namespace Cartservice_namespace
{
	public class MycartService : Icart
	{
		private readonly DBconn _dbconn;
		private ExternalAPI _externalApi;
		private readonly Jwt _jwt;
		public MycartService(DBconn dbconn,ExternalAPI externalAPI,Jwt jwt)
		{
			_dbconn = dbconn;
			_externalApi = externalAPI;
			_jwt=jwt;
		}
		public async Task<String> AddToCart(int productId,String token,int quantity)
		{
			String userId=_jwt.GetUserIdFromToken(token);
			ProductModel productModel= await _externalApi.GetProductFromProductService(productId);
			if(productModel!=null)
			{
				CartModel cartModel = new CartModel()
				{
					price = productModel.price,
					productId = $"{productId}", 
					productCart = productModel,
					dateAdded = DateTime.Now,
					userId = userId,
					quantity = quantity

				};

				_dbconn.cart.Add(cartModel);
				_dbconn.SaveChanges();
				return "added to cart successfully";

			}else
			{
				return "No product found";
			}
			
			

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