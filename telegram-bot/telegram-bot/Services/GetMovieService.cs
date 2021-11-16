using System.Threading.Tasks;
using Newtonsoft.Json;
using TelegramBot.WebHookSetup;
using TelegramBot.Types;
using Newtonsoft.Json.Linq;
using System;

namespace TelegramBot.Services.GetMovieService
{
    public class GetMvie : IStrategy
	{
	/*
        private readonly IWebHookClient _client;
        private readonly BotConfiguration _botConfig;
        private readonly string _title;

        public GetMovieService(BotConfiguration botConfig, string[] title, IWebHookClient client)
        {
            _botConfig = botConfig;
            _title= String.Join(" ", title);
            _client = client;
        }
		*/
	
		public async Task SendMessage(IWebHookClient client, Update update)
		{
			string title = update.Message.Text;
			string[] word = title.Split(' ');
			string messageResponse;
			var response = client.GetMovieIdByTitle(word[1]);
			

			var answer = response.Result;
			var u = FormatToJSON(answer.ToString());
			JObject ds =JObject.Parse(u);
			MovieSearchResults deserializedJsonResponse = JsonConvert.DeserializeObject<MovieSearchResults>(ds);
			if (deserializedJsonResponse != null)
			{
				messageResponse = u;				
			}
			else
			{
				messageResponse ="фильм не нашли нахуй";			
			}
			await client.SendTextMessage(update.Message.Chat.Id, messageResponse); 
		}

       
		static string FormatToJSON(string json)
        {
            try
            {
                object dontBeJSON = JsonConvert.DeserializeObject(json);
                string beJSON = JsonConvert.SerializeObject(dontBeJSON, Formatting.Indented);



                return beJSON;
            }
            catch
            {
                return "ERROR_WITH_METHOD_FormatToJSON";
            }
        }
    }


}
