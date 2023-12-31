using EmailService.Models;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;

namespace EmailService.Services
{
	public class MailService
	{
		private readonly IConfiguration _configuration;

		public MailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		internal async Task SendWelcomeMessage(UserDetailsDTO userDetails)
		{
			String useremail = userDetails.Email;
			String username = userDetails.username;
			Console.WriteLine($"{useremail} {username}");
			try
			{
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress("LeonMicroservice", "leonteqsec@gmail.com"));
				message.To.Add(new MailboxAddress(username, useremail));
				message.Subject = "WELCOME";
				message.Body = new TextPart("plain")
				{
					Text = $"Hello, {username},Welcome to Leon microservice.You account was created successfuly" +
					$"you can now login to our system using the username and password you registered with."
				};

				var smtpHost = _configuration["SmtpConfig:Host"];
				var smtpPort = int.Parse(_configuration["SmtpConfig:Port"]);
				var smtpUser = _configuration["SmtpConfig:Username"];
				var smtpPassword = _configuration["SmtpConfig:Password"];

				using (var client = new SmtpClient())
				{
					client.Connect(smtpHost, smtpPort, false);
					client.Authenticate(smtpUser, smtpPassword);

					await client.SendAsync(message);
					client.Disconnect(true);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
