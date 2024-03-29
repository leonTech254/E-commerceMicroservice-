using DBconnection_namespace;
using Microsoft.AspNetCore.Mvc;
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
			DateTime dateTime = DateTime.Now;
			product.dateAdded = dateTime.ToString();
			_dBconn.products.AddAsync(product);
			_dBconn.SaveChanges();
			return "Product Added successfully " +product.ToString();
		}

		public string DeleteProduct(int id)
		{
			List<ProductModel> productModels =getAllProducts();
			ProductModel productModel= productModels.FirstOrDefault(e=>e.Id==id);
			if(productModel!=null)
			{
			_dBconn.products.Remove(productModel);
			_dBconn.SaveChanges();
			return "deleted successfully";
			}else
			{
				return "null";
			}

		
		}

		public List<ProductModel> getAllProducts()
		{
			List<ProductModel> allproducts=_dBconn.products.ToList();
			return allproducts;
		}

		public ProductModel getProductById(int productid)
		{
			List<ProductModel> productModelsList = _dBconn.products.ToList();
			ProductModel dbproduct =productModelsList.FirstOrDefault(e=>e.Id==productid) ;
			return dbproduct;
		}

		public string UpdateProduct(int productid, ProductModel updatedProduct)
		{
			ProductModel existingProduct = getProductById(productid);

			if (existingProduct != null)
			{
				existingProduct.price = updatedProduct.price;
				existingProduct.Description = updatedProduct.Description;
				existingProduct.Name = updatedProduct.Name;
				

				// Save changes to the context
				_dBconn.SaveChanges();

				return "Updated successfully";
			}
			else
			{
				return "Item not Found";
			}
		}

	}


}