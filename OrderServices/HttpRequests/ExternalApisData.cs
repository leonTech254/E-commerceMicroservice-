using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using OrderModel_namespace;
using static Microsoft.Azure.Amqp.CbsConstants;
using CartModel_namespace;
using System.Text;
using RewardModel_namespace;

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

		public async Task RewardPoints(RewardModel rewardModel, string authToken)
		{
			string url = "https://localhost:7130/api/rewards/user";

			// Serialize the RewardModel to JSON
			string jsonContent = JsonSerializer.Serialize(rewardModel);

			// Create the HTTP request content with JSON data
			HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			// Set the authorization header
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

			// Send the POST request
			HttpResponseMessage response = await client.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				// Handle success (if needed)
				Console.WriteLine("Successfully sent the POST request");
			}
			else
			{
				// Handle error (if needed)
				Console.WriteLine($"Error sending POST request. Status Code: {response.StatusCode}");
			}
		}





	}
}
