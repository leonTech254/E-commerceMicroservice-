namespace CartModel_namespace
{
	public class CartModel
	{
		public CartModel() { }
		public int Id { get; set; }
		public string productId { get; set; }
		public DateTime dateAdded { get; set; }
		public String userId {  get; set; }
		public int quantity { get; set; }
		public int price { get; set; }
	}
}