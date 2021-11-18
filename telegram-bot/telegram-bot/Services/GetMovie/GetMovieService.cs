using System.Threading.Tasks;
using TelegramBot.WebHookSetup;
using TelegramBot.Types;
using System.Collections.Generic;

namespace TelegramBot.Services.GetMovie
{
    public class GetMovieService : IStrategy
	{
		public async Task SendMessage(IWebHookClient client, Update update)
		{
			string title = update.Message.Text;
			title = title.Replace("/getmovie", "").Trim(' ');
			string messageResponse;

			if (title != "")
            {
                var results = await client.GetMovieIdByTitle(title);
				if (results.Count == 0)
					messageResponse = "Фильм не найден";
				else
					messageResponse = $"Movie: {results[0].Title}";
            }
            else
            {
                messageResponse = "Введите название фильма после команды /getmovie";
            }


			await client.SendTextMessage(update.Message.Chat.Id, messageResponse);
        }
    }


}
