using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.WebHookSetup
{
	public class WebHookClient : IWebHookClient
	{
		private readonly BotConfiguration _botConfig;
		private readonly HttpClient _httpClient;
		private readonly ILogger<WebHookClient> _logger;

		public WebHookClient(ILogger<WebHookClient> logger,
							BotConfiguration BotConfig,
							HttpClient httpClient)
		{
			_botConfig = BotConfig;
			_httpClient = httpClient;
			_logger = logger;
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

			_logger.LogInformation($"Message: '{message}' was sent to chat with id: {chatId}");

			await _httpClient.PostAsync(@$"{_botConfig.HostAddress}{_botConfig.BotToken}/sendMessage", content);
		}
	}
}
