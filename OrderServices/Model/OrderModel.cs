namespace OrderModel_namespace
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string productId { get; set; }
		public DateTime datePlaced { get; set; }
		public String userId { get; set; }
		public int  quantity { get; set; }
		public int payAmount { get; set; }
	}
}