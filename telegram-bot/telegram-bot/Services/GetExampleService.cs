using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TelegramBot.Types;
using TelegramBot.WebHookSetup;

namespace TelegramBot.Services
{
    public class GetExampleService : IStrategy
    {
        public async Task SendMessage(IWebHookClient client, Update update)
        {
            string messageResponse = "this message is an example of a response";
            await client.SendTextMessage(update.Message.Chat.Id, messageResponse);
        }
    }
}
