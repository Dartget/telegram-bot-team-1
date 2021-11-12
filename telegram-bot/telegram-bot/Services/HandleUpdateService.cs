using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using telegram_bot.WebHookSetup;

namespace telegram_bot.Services
{
    public class HandleUpdateService
    {
        private readonly IWebHookClient _botClient;
        private readonly ILogger<HandleUpdateService> _logger;

        public HandleUpdateService(IWebHookClient botClient, ILogger<HandleUpdateService> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }

        public async Task HandlerAsync(Update update)
        {
            var handler = update.message.text switch
            {
                "/one" => new Context(new Command()),
                "/two" => new Context(new Command()),
                _ => new Context(new IncorrectMessage())
            };

            try
            {
                await handler.SendMessage(_botClient, update);
            }
            catch (Exception exception)
            {
                await HandlerErrorAsync(exception);
            }
        }

        public Task HandlerErrorAsync(Exception exception)
        {
            _logger.LogInformation("Error with HandleUpdateService: {Message}", exception.Message);
            return Task.CompletedTask;
        }
    }
}
