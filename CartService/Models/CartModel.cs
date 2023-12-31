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
		public int price { get; set; }
		public  ProductModel productCart;
	}
}