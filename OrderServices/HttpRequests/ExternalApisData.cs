using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using OrderModel_namespace;
using static Microsoft.Azure.Amqp.CbsConstants;
using CartModel_namespace;

namespace ExternalApiData_namespace
{
	public class ExternalAPI
	{
		private readonly HttpClient client;

		public ExternalAPI()
		{
			client = new HttpClient();
		}

		public async Task<CartModel> GetCartFromCartService(int id,String AuthToken)
		{
			string url= "https://localhost:7064/api/cart/GetById/" + id;
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);
			HttpResponseMessage response = await client.GetAsync(url);
			Console.WriteLine("Successfully calling");
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Successfully 200");
				string data = await response.Content.ReadAsStringAsync();
				Console.WriteLine("hello leon"+data);

				CartModel cart = JsonSerializer.Deserialize<CartModel>(data);
				return cart;
			}
			else
			{
				// Consider logging or handling the error in a more specific way
				return null;
			}
		}
	}
}
