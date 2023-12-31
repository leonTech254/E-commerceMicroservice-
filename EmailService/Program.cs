using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmailService;
using EmailService.Services;

class Program
{
	static void Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
			{
				services.AddHostedService<AzureServices>();
				services.AddSingleton<MailService>();
			});
}
