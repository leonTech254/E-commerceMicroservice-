using ProductModel_namespace;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartModel_namespace
{
	public class CartModel
	{
		public CartModel() { }
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string productId { get; set; }
		public DateTime dateAdded { get; set; }
		public String userId {  get; set; }
		public int quantity { get; set; }
		public int pricePerItem { get; set; }
		public int totalprice { get; set; }
		public  ProductModel productCart;
	}
}