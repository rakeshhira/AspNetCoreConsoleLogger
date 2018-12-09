using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
	internal class TimedHostedService : IHostedService, IDisposable
	{
		private readonly ILogger _logger;
		private Timer _timer;

		public TimedHostedService(ILogger<TimedHostedService> logger)
		{
			_logger = logger;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Timed Background Service is starting.");

			_timer = new Timer(DoWork, null, TimeSpan.Zero,
				TimeSpan.FromSeconds(5));

			return Task.CompletedTask;
		}

		private void DoWork(object state)
		{
			var dateTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
			var message = $"Timed Background Service is working.";
			using (_logger.BeginScope("--------scope-----------"))
			{
				_logger.LogTrace("{dateTime} My Trace message {message}", dateTime, message);
				_logger.LogInformation("{dateTime} My Information message {message}", dateTime, message);
				_logger.LogError("{dateTime} My Error message {message}", dateTime, message);
				_logger.LogCritical("{dateTime} My Critical message {message}", dateTime, message);
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Timed Background Service is stopping.");
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}