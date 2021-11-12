using System.Threading.Tasks;
using telegram_bot.WebHookSetup;

namespace telegram_bot.Services
{
    public interface IStrategy
    {
        public Task SendMessage(IWebHookClient client, Update update);
    }
}