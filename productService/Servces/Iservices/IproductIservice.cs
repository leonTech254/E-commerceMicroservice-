using ProductModel_namespace;

namespace ProductServiceInterface_namespace
{
	public interface Iproducts
	{
		List<ProductModel> getAllProducts();
		String AddProduct(ProductModel product);
		String UpdateProduct(ProductModel product);
		String DeleteProduct(ProductModel product);

	}
}