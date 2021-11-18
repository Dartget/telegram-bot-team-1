using System;
using System.Threading.Tasks;
using TelegramBot.WebHookSetup;
using TelegramBot.Types;

namespace TelegramBot.Services.Weather
{
    public class WeatherService : IStrategy
    {

        static float temperature_in_city; //температура
        static string name_of_city; //название города для вывода

        static float temperature_feeling; // ощущается как
        static string wind_speed; // скорость ветра
        static int date_and_time;

        public async Task SendMessage(IWebHookClient client, Update update)
        {
			string city = update.Message.Text;
			city = city.Replace("/getweather", "").Trim(' ');
			string messageResponse;
			if (city != "")
			{
				try
				{
					WeatherResponse weatherResponse = await client.GetWeatgerByCity(city);
					name_of_city = weatherResponse.Name;
					temperature_in_city = weatherResponse.Main.Temp - 273;
					temperature_feeling = weatherResponse.Main.Feels_like - 273;
					wind_speed = weatherResponse.Wind.Speed;
					date_and_time = weatherResponse.DT;
					messageResponse =
						$"Статистика по городу {name_of_city} на {Data(date_and_time)}\n" +
						$"Температура: {Math.Round(temperature_in_city)}°C \n " +
						$"Ощущается как: {Math.Round(temperature_feeling, 1)}°C\n" +
						$"Скорость ветра: {wind_speed}";
				}
				catch (Exception)
				{
					messageResponse = $"Город {city} не найден ";
				}

			}
			else
				messageResponse = "Введите название города после /getweather";

			await client.SendTextMessage(update.Message.Chat.Id, messageResponse);
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
