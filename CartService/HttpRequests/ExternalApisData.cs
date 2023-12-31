using ProductModel_namespace;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace ExternalApiData_namespace
{
	public class ExternalAPI
	{
		private readonly HttpClient client;

		public ExternalAPI()
		{
			client = new HttpClient();
		}

		public async Task<ProductModel> GetProductFromProductService(int id)
		{
			string url= "https://localhost:7257/api/v1/products/getProductById/"+id;
			HttpResponseMessage response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				ProductModel product = JsonSerializer.Deserialize<ProductModel>(data);
				return product;
			}
			else
			{
				// Consider logging or handling the error in a more specific way
				return null;
			}
		}
	}
}
