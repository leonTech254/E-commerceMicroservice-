using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using EmailService.Models;

namespace EmailService.Services
{
	public class AzureServices : BackgroundService
	{
		private readonly IConfiguration _configuration;
		private readonly MailService _mailService;

		public AzureServices(IConfiguration configuration, MailService mailService)
		{
			_configuration = configuration;
			_mailService = mailService;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			string connectionString = _configuration.GetSection("AzureServices:connectionString").Value;
			string queueName = _configuration.GetSection("AzureServices:qName").Value;

			await using (ServiceBusClient client = new ServiceBusClient(connectionString))
			{
				// Create a receiver for the queue
				ServiceBusReceiver receiver = client.CreateReceiver(queueName);

				while (!stoppingToken.IsCancellationRequested)
				{
					try
					{
						// Receive messages
						ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

						if (receivedMessage != null)
						{
							string messageBody = Encoding.UTF8.GetString(receivedMessage.Body);
							Console.WriteLine($"Received a message from the queue: {messageBody}");

							// Complete the message to remove it from the queue
							await receiver.CompleteMessageAsync(receivedMessage);
							await _mailService.SendWelcomeMessage(GetUsernameEmail(messageBody));
						}
						else
						{
							Console.WriteLine("No messages available in the queue. Waiting for new messages...");
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error receiving message: {ex.Message}");
					}

					// Add a delay before checking for new messages again
					await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
				}
			}
		}

		public UserDetailsDTO? GetUsernameEmail(String userDetails)
		{
			UserDetailsDTO UserData = JsonConvert.DeserializeObject<UserDetailsDTO>(userDetails);
			// Getting useremail
			try
			{
				String useremail = UserData.Email;
				String Username = UserData.username;
				return UserData;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return null;
			}
		}
	}
}
