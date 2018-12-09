using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = new HostBuilder()
				.ConfigureAppConfiguration((hostingContext, config) =>
				{
					var env = hostingContext.HostingEnvironment;
					config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
						.AddJsonFile($"appsettings.{env.EnvironmentName}.json",
							optional: true, reloadOnChange: true);
					config.AddEnvironmentVariables();
				})
				.ConfigureLogging((hostingContext, logging) =>
				{
					logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
					logging.AddMyConsole();
					//logging.AddConsole();
					logging.AddDebug();
					logging.AddEventSourceLogger();
				})
				.ConfigureServices(config =>
				{
					config.AddHostedService<TimedHostedService>();
				});

			await host.RunConsoleAsync();
		}
	}
}
