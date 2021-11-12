using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace telegram_bot.WebHookSetup
{
	public class ConfigureWebhook : IHostedService
	{
		private readonly ILogger<ConfigureWebhook> _logger;
		//private readonly IServiceProvider _services;
		private readonly BotConfiguration _botConfig;
		private readonly Singleton _singleton;

		public ConfigureWebhook(ILogger<ConfigureWebhook> logger,
								/*IServiceProvider serviceProvider,*/
								IConfiguration configuration,
								Singleton singleton)
		{
			_logger = logger;
			//_services = serviceProvider;
			_botConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
			_singleton = singleton;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			var webhookAddress = @$"{_botConfig.HostAddress}{_botConfig.BotToken}/setWebhook?url={_botConfig.NgrokAddress}/api/BotController";
			_logger.LogInformation($"Setting webhook:  {webhookAddress}");
			await _singleton.SetWebhookAsync(url: webhookAddress, cancellationToken: cancellationToken);
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			var webhookAddress = @$"{_botConfig.HostAddress}{_botConfig.BotToken}/deleteWebhook";
			_logger.LogInformation("Removing webhook");
			await _singleton.DeleteWebhookAsync(url: webhookAddress, cancellationToken: cancellationToken);
		}
	}
}
