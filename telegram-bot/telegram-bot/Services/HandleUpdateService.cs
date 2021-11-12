using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using telegram_bot.Types;
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
            string message = update.Message.Text;
            string[] word = message.Split(' ');

            var handler = word[0] switch
            {
                "/example" => new Context(new ExampleService()),
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
