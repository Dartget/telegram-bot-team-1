using System.Threading.Tasks;
using TelegramBot.Types;
using TelegramBot.WebHookSetup;

namespace TelegramBot.Services
{
    public interface IStrategy
    {
        public Task SendMessage(IWebHookClient client, Update update);
    }
}
