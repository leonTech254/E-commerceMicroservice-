using DBconnection_namespace;
using ProductModel_namespace;
using ProductServiceInterface_namespace;

namespace ProductService_namespace
{
	public class ProductService :Iproducts
	{
		private readonly DBconn _dBconn;
		public ProductService(DBconn dBconn) {
		_dBconn=dBconn;
		}

		public string AddProduct(ProductModel product)
		{
		_dBconn.products.AddAsync(product);
		_dBconn.SaveChanges();
			return "Product Added successfully " +product.ToString();
		}

		public string DeleteProduct(ProductModel product)
		{
			_dBconn.products.Remove(product);
			_dBconn.SaveChanges();
			return "deleted successfully";
		}

		public List<ProductModel> getAllProducts()
		{
			List<ProductModel> allproducts=_dBconn.products.ToList();
			return allproducts;
		}

		public string UpdateProduct(ProductModel product)
		{
			// Assuming you have some logic to update the product
			_dBconn.products.Update(product);
			_dBconn.SaveChanges();
			return "Updated successfully";
		}
	}


}