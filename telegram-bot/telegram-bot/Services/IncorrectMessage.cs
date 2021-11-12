using System.Threading.Tasks;
using telegram_bot.Types;
using telegram_bot.WebHookSetup;

namespace telegram_bot.Services
{
    public class IncorrectMessage : IStrategy
    {
        public async Task SendMessage(IWebHookClient client, Update update)
        {
            string messageResponse = "incorrect message";
            await client.SendTextMessage(update.Message.Chat.Id, messageResponse);
        }
    }
}
