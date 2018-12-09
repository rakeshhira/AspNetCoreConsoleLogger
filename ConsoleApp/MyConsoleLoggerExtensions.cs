using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace ConsoleApp
{
	public static class MyConsoleLoggerExtensions
	{
		public static ILoggingBuilder AddMyConsole(this ILoggingBuilder builder)
		{
			builder.AddConfiguration();

			builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, MyConsoleLoggerProvider>());
			builder.Services.TryAddEnumerable(ServiceDescriptor
				.Singleton<IConfigureOptions<MyConsoleLoggerOptions>, MyConsoleLoggerOptionsSetup>());
			builder.Services.TryAddEnumerable(ServiceDescriptor
				.Singleton<IOptionsChangeTokenSource<MyConsoleLoggerOptions>, LoggerProviderOptionsChangeTokenSource<
					MyConsoleLoggerOptions, MyConsoleLoggerProvider>>());

			return builder;
		}
	}
}