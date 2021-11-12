using System.Threading.Tasks;
using telegram_bot.WebHookSetup;

namespace telegram_bot.Services
{
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