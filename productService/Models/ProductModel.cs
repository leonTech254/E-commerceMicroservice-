using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModel_namespace
{
	public class ProductModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string imageUrl {get;set;}
		public string Description { get; set; }
		public string dateAdded { get; set; }
		public int price { get; set; }


	}
}