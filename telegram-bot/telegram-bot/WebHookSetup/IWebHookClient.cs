using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace telegram_bot.WebHookSetup
{
    public interface IWebHookClient
    {
        // funs for dev
        Task SetWebhook(string url);
        Task DeleteWebhook();
        Task SendTextMessage(int chatId, string message);
    }
}