using System.Threading.Tasks;
using TelegramBot.WebHookSetup;
using TelegramBot.Types;


namespace TelegramBot.Services.GetMovie
{
    public class GetMovieService : IStrategy
	{	
		public async Task SendMessage(IWebHookClient client, Update update)
		{
			string title = update.Message.Text;
			string[] word = title.Split(' ');
			string messageResponse;
            if (word.Length > 1)
            {
                var results = await client.GetMovieIdByTitle(word[1]);                
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
