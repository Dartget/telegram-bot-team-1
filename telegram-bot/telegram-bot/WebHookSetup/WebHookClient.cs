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

    public class WebHookClient : IWebHookClient
    {
        private const string BaseUrl = "https://api.telegram.org/bot";

        private readonly string _baseRequestUrl;

        private readonly string _token;

        private readonly HttpClient _httpClient;

        public WebHookClient(string token, HttpClient httpClient = null)
        {
            _token = token;
            _baseRequestUrl = $"{BaseUrl}{_token}";
            _httpClient = httpClient ?? new HttpClient();
        }

        public async Task SetWebhook(string url)
        {
            await _httpClient.GetAsync(url);
        }

        public async Task DeleteWebhook()
        {
            await _httpClient.GetAsync(_baseRequestUrl + "/deleteWebhook");
        }
        public async Task SendTextMessage(int chatId, string message)
        {
            var Params = new Dictionary<string, string>()
            {
                { "chat_id", chatId.ToString() },
                { "text", message }
            };
            string json = JsonConvert.SerializeObject(Params);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync(_baseRequestUrl + "/sendMessage", content);
        }
    }
}
