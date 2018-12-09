using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace ConsoleApp
{
	internal class MyConsoleLoggerOptionsSetup : ConfigureFromConfigurationOptions<MyConsoleLoggerOptions>
	{
		public MyConsoleLoggerOptionsSetup(ILoggerProviderConfiguration<MyConsoleLoggerProvider> providerConfiguration)
			: base(providerConfiguration.Configuration)
		{
		}
	}
}
