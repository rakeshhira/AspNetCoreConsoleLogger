using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ConsoleApp
{
	[ProviderAlias("MyConsole")]
	public class MyConsoleLoggerProvider : ILoggerProvider
	{
		private MyConsoleLoggerOptions _options;
		private readonly ConcurrentDictionary<string, MyConsoleLogger> _loggers = new ConcurrentDictionary<string, MyConsoleLogger>();
		private readonly IDisposable _optionsReloadToken;

		public MyConsoleLoggerProvider(IOptionsMonitor<MyConsoleLoggerOptions> options)
		{
			_optionsReloadToken = options.OnChange(ReloadLoggerOptions);
			ReloadLoggerOptions(options.CurrentValue);
		}

		private void ReloadLoggerOptions(MyConsoleLoggerOptions options)
		{
			string json = JsonConvert.SerializeObject(options);
			_options = JsonConvert.DeserializeObject<MyConsoleLoggerOptions>(json);
		}

		private MyConsoleLogger CreateLogger(string categoryName)
		{
			return _loggers.GetOrAdd(categoryName, name => new MyConsoleLogger(name, _options));
		}

		// ILoggerProvider implementation
		ILogger ILoggerProvider.CreateLogger(string categoryName) => CreateLogger(categoryName);

		// IDisposable implementation
		void IDisposable.Dispose() => _optionsReloadToken?.Dispose();
	}
}
