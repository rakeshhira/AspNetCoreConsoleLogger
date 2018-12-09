using System;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
	public class MyConsoleLogger : ILogger
	{
		private readonly string _name;
		private readonly MyConsoleLoggerOptions _options;

		public MyConsoleLogger(string name, MyConsoleLoggerOptions options)
		{
			_name = name;
			_options = options;
		}

		// ILogger implementation
		IDisposable ILogger.BeginScope<TState>(TState state) => null;
		bool ILogger.IsEnabled(LogLevel logLevel) => true;
		void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) => WriteLog(logLevel, state, exception, formatter);

		private static void WriteLog<TState>(LogLevel logLevel, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (formatter == null)
			{
				throw new ArgumentNullException(nameof(formatter));
			}

			var message = formatter(state, exception);
			if (string.IsNullOrEmpty(message) && exception == null)
			{
				return;
			}

			var logBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(message))
			{
				logBuilder.AppendLine(message);
			}

			if (exception != null)
			{
				logBuilder.AppendLine(exception.ToString());
			}

			if (logLevel != LogLevel.None)
			{
				Console.Out.Write($"{logLevel.ToString()}:");
			}

			Console.Out.Write(logBuilder.ToString());
			Console.Out.Flush();
		}
	}
}
