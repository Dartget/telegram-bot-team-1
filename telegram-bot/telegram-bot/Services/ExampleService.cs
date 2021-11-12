using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using telegram_bot.Types;
using telegram_bot.WebHookSetup;

namespace telegram_bot.Services
{
    public class ExampleService : IStrategy
    {
        public async Task SendMessage(IWebHookClient client, Update update)
        {
            string messageResponse = "this message is an example of a response";
            await client.SendTextMessage(update.Message.Chat.Id, messageResponse);
        }
    }
}
