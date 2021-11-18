using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TelegramBot.WebHookSetup
{
	public class ConfigureWebhook : IHostedService
	{
		private readonly ILogger<ConfigureWebhook> _logger;
		private readonly BotConfiguration _botConfig;
		private readonly ISetupClient _setupClient;

		public ConfigureWebhook(ILogger<ConfigureWebhook> logger,
								IConfiguration configuration,
								ISetupClient setupClient)
		{
			_logger = logger;
			_botConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
			_setupClient = setupClient;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			var webhookAddress = @$"{_botConfig.HostAddress}{_botConfig.BotToken}/setWebhook?url={_botConfig.NgrokAddress}/api/BotController";
			_logger.LogInformation($"Setting webhook:  {webhookAddress}");
			await _setupClient.SetWebhookAsync(url: webhookAddress, cancellationToken: cancellationToken);
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			var webhookAddress = @$"{_botConfig.HostAddress}{_botConfig.BotToken}/deleteWebhook";
			_logger.LogInformation("Removing webhook");
			await _setupClient.DeleteWebhookAsync(url: webhookAddress, cancellationToken: cancellationToken);
		}
	}
}
