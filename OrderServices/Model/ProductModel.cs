using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductModel_namespace
{
	public class ProductModel
	{
		public int Id { get; set; }
		public string productId { get; set; }
		public string dateAdded { get; set; }
		public string userId { get; set; }
		public int quantity { get; set; }
		public int price { get; set; }
	}
}