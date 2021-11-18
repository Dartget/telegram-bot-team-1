using System.Threading.Tasks;
using TelegramBot.Types;
using TelegramBot.WebHookSetup;

namespace TelegramBot.Services
{
    public class GetIncorrectMessage : IStrategy
    {
        public async Task SendMessage(IWebHookClient client, Update update)
        {
            string messageResponse = "Несуществующая комманда, для просмотра всех возможных команд напишите /help";
            await client.SendTextMessage(update.Message.Chat.Id, messageResponse);
        }
    }
}
