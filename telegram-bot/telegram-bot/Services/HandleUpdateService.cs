using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TelegramBot.Types;
using TelegramBot.WebHookSetup;

namespace TelegramBot.Services
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

		public Context CreateContext(Update update)
		{
			string message = update.Message.Text;
			string[] word = message.Split(' ');

			var handler = word[0] switch
			{
				"/example" => new Context(new GetExampleService()),
				_ => new Context(new GetIncorrectMessage())
			};
			return handler;
		}

		public async Task HandlerAsync(Update update)
        {
			var handler = CreateContext(update);

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
