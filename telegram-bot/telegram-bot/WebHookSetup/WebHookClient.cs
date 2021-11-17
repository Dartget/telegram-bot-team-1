using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using TelegramBot.Services.GetMovie;

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
		public async Task<IList<MovieResult>> GetMovieIdByTitle(string title)
		{
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"{_botConfig.ImdbApiUrl}{title}/"),
				Headers =
				{
					{ "x-rapidapi-host", "data-imdb1.p.rapidapi.com"},
					{ "x-rapidapi-key", _botConfig.RapidApiKey},
				},
			};
			_logger.LogInformation($"Request to movie api {request}");

			using (var response = await _httpClient.SendAsync(request))
			{
				JObject movieSearch = JObject.Parse(await response.Content.ReadAsStringAsync());
				JEnumerable<JToken> results = movieSearch["results"].Children();
				IList<MovieResult> movieResults = new List<MovieResult>();

				foreach (JToken result in results)
				{
					MovieResult movieResult = result.ToObject<MovieResult>();
					movieResults.Add(movieResult);
				}
				return movieResults;
			}			
		}
	}
}
