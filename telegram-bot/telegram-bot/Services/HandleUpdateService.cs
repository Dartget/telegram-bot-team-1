using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using telegram_bot.Services.Weather;
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
            string message = update.message.text;
            string[] word = message.Split(' ');

            var handler = word[0] switch
            {
                "/one" => new Context(new Command()),
                "/two" => new Context(new Command()),
                "/weather" => new Context(new WeatherService()),
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
            _logger.LogInformation("Error with HandleUpdateService: ", exception.Message);
            return Task.CompletedTask;
        }
    }

    public interface IStrategy
    {
        public Task SendMessage(IWebHookClient client, Update update);
    }

    public class IncorrectMessage : IStrategy
    {

        public async Task SendMessage(IWebHookClient client, Update update)
        {
            string messageResponse = "incorrect message";
            await client.SendTextMessage(update.message.chat.id, messageResponse);
        }
    }

    public class Command : IStrategy
    {
        public async Task SendMessage(IWebHookClient client, Update update)
        {
            var action = update.message.text switch
            {
                "/one" => "you choice command 1",
                "/two" => "you choice command 2"
            };

            string messageResponse = action;
            await client.SendTextMessage(update.message.chat.id, messageResponse);
        }
    }

    public class Context
    {
        public IStrategy strategy { get; set; }

        public Context(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        public async Task SendMessage(IWebHookClient client, Update update)
        {
            await this.strategy.SendMessage(client, update);
        }
    }
}
