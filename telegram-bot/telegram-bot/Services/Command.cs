using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using telegram_bot.WebHookSetup;

namespace telegram_bot.Services
{
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
}