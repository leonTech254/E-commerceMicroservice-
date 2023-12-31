using ProductModel_namespace;

namespace ProductServiceInterface_namespace
{
	public interface Iproducts
	{
		List<ProductModel> getAllProducts();
		String AddProduct(ProductModel product);
		String UpdateProduct(int productid,ProductModel product);
		String DeleteProduct(ProductModel product);
		ProductModel getProductById(int productid);

	}
}