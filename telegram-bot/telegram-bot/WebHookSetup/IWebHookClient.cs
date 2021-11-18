using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.Services.GetMovie;
using TelegramBot.Services.Weather;

namespace TelegramBot.WebHookSetup
{
	public interface IWebHookClient
	{
		// funs for dev
		Task SendTextMessage(int chatId, string message);
		Task<IList<MovieResult>> GetMovieIdByTitle(string title);
		Task<WeatherResponse> GetWeatgerByCity(string city);
	}
}
