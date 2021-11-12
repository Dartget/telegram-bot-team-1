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
        private readonly IServiceProvider _services;
        private readonly BotConfiguration _botConfig;
        private readonly string _ngrokAddress;

        public ConfigureWebhook(ILogger<ConfigureWebhook> logger,
                                IServiceProvider serviceProvider,
                                IConfiguration configuration)
        {
            _logger = logger;
            _services = serviceProvider;
            _botConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<IWebHookClient>();

            var webhookAddress = @$"{_botConfig.HostAddress}{_botConfig.BotToken}/setWebhook?url={_botConfig.UrlPath}/api/BotController";
            _logger.LogInformation("Setting webhook: ", webhookAddress);
            await botClient.SetWebhook(url: webhookAddress);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<IWebHookClient>();

            _logger.LogInformation("Removing webhook");
            await botClient.DeleteWebhook();
        }
    }
}
