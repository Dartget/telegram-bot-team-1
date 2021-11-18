using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TelegramBot.Types;
using TelegramBot.WebHookSetup;

namespace TelegramBot.Services
{
    public class GetCommandsService : IStrategy
    {
        public async Task SendMessage(IWebHookClient client, Update update)
        {
            string messageResponse = "Доступные команды\n" +
				"/getmovie <название фильма>\n" +
				"/getweather <название городв>\n"
				;
            await client.SendTextMessage(update.Message.Chat.Id, messageResponse);
        }
    }
}
