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
				message.From.Add(new MailboxAddress("SocialApp", "leonteqsec@gmail.com"));
				message.To.Add(new MailboxAddress(username, useremail));
				message.Subject = $"Hello, {username}, you are welcome to our Social App";
				message.Body = new TextPart("plain")
				{
					Text = "This is the body of the email.",
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
