using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrderDTO_namespace;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceNamespace
{
	public class MessageService
	{
		private readonly IConfiguration _configuration;

		public MessageService(IConfiguration configuration)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}
		public async Task<ActionResult> AddOrderToQueue(OrderDTO order)
		{
			string connectionString = _configuration.GetSection("AzureServices:connectionString").Value;
			string queueName = _configuration.GetSection("AzureServices:qName").Value;
			string userBody = JsonConvert.SerializeObject(order);

			await using (ServiceBusClient client = new ServiceBusClient(connectionString))
			{
				// Create a sender for the queue
				ServiceBusSender sender = client.CreateSender(queueName);

				// Create a message
				ServiceBusMessage message = new ServiceBusMessage(Encoding.UTF8.GetBytes(userBody));

				// Send the message
				await sender.SendMessageAsync(message);

				Console.WriteLine($"User Added to Queue Successfully: {userBody}");

				return new OkObjectResult($"USer {userBody} added successfully");
			}
		}

        internal async Task<ActionResult> AddRewardToQueue(OrderDTO orderDTO)
        {
           string connectionString = _configuration.GetSection("AzureServices:connectionString").Value;
			string queueName = _configuration.GetSection("AzureServices:qrewards").Value;
			string userBody = JsonConvert.SerializeObject(orderDTO);

			await using (ServiceBusClient client = new ServiceBusClient(connectionString))
			{
				// Create a sender for the queue
				ServiceBusSender sender = client.CreateSender(queueName);

				// Create a message
				ServiceBusMessage message = new ServiceBusMessage(Encoding.UTF8.GetBytes(userBody));

				// Send the message
				await sender.SendMessageAsync(message);

				Console.WriteLine($"User Added to Queue Successfully: {userBody}");

				return new OkObjectResult($"USer {userBody} added successfully");
			}
        }
    }
}