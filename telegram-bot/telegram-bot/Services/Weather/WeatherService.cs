using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TelegramBot.WebHookSetup;
using TelegramBot.Types;
namespace TelegramBot.Services.Weather
{
    class WeatherService : IStrategy
    {
        static string name_city; // название города
        static float temperature_in_city; //температура
        static string name_of_city; //название города для вывода

        static float temperature_feeling; // ощущается как
        static string wind_speed; // скорость ветра
        static int date_and_time;

		private readonly BotConfiguration _botConfig;

		static string[] _a;

        public WeatherService(string[] a, BotConfiguration botConfig)
        {
            _a = a;
			_botConfig = botConfig;
		}
        


        public async Task SendMessage(IWebHookClient client, Update update)
        {
					
			if (_a.Length < 3)
			{
				name_city = _a[1];

			}
			else
			{
				name_city = _a[1] + " " + _a[2];
			}
			
			


			Weather(name_city);

            await client.SendTextMessage(update.Message.Chat.Id, $"Статистика по городу {name_of_city} на {Data(date_and_time)}\nТемпература: {Math.Round(temperature_in_city)} °C \n Ощущается как: {Math.Round(temperature_feeling, 1)} °C\n Скорость ветра: {wind_speed}");

        }
        public  void Weather(string cityName)
        {

            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&unit=metric&appid="+ _botConfig.WeatherToken + "&lang=ru";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);                                 
HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest?.GetResponse();
            string response;        

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

            name_of_city = weatherResponse.Name;
            temperature_in_city = weatherResponse.Main.Temp - 273;
            temperature_feeling = weatherResponse.Main.Feels_like - 273;
            wind_speed = weatherResponse.Wind.Speed;
            date_and_time = weatherResponse.DT;

        }

        public static string Data(int D)
        {
            DateTime data = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            data = data.AddSeconds(D);
            data = data.ToLocalTime();

            return data.ToString("dd-MM-yyyy \nВремя обновления данных: hh:mm:ss");
        }
    }
}
