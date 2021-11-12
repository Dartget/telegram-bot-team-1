using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using telegram_bot.WebHookSetup;

namespace telegram_bot.Services.Weather
{
    public class WeatherService : IStrategy
    {
        static string NameCity; // название города
        static float tempOfCity; //температура
        static string nameOfCity; //название города для вывода

        static float Feeling; // ощущается как
        static string wind_speed; // скорость ветра
        static int time;

        public async Task SendMessage(IWebHookClient client, Update update)
        {
            string b = update.message.text;
            string[] a = b.Split(' ');

            NameCity = a[1];
            Weather(NameCity);

            await client.SendTextMessage(update.message.chat.id, $"Статистика по городу {nameOfCity} на {Data(time)}\nТемпература: {Math.Round(tempOfCity)} °C \n Ощущается как: {Math.Round(Feeling, 1)} °C\n Скорость ветра: {wind_speed}");
        }
        public static void Weather(string cityName)
        {

            string url = "https://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&unit=metric&appid=2351aaee5394613fc0d14424239de2bd&lang=ru";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest?.GetResponse();
            string response;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

            nameOfCity = weatherResponse.Name;
            tempOfCity = weatherResponse.Main.Temp - 273;
            Feeling = weatherResponse.Main.Feels_like - 273;
            wind_speed = weatherResponse.Wind.Speed;
            time = weatherResponse.DT;

        }

        public static string Data(int D)
        {
            DateTime data = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            data = data.AddSeconds(D);
            data = data.ToLocalTime();

            return data.ToString("dd-MM-yyyy \nВремя: hh:mm:ss");
        }
    }
}
