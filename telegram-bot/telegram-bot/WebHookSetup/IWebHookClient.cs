using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBot.Services.GetMovie;

namespace TelegramBot.WebHookSetup
{
	public interface IWebHookClient
	{
		// funs for dev
		Task SendTextMessage(int chatId, string message);
		Task<IList<MovieResult>> GetMovieIdByTitle(string title);
	}
}
