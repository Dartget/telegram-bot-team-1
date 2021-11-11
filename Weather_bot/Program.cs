using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace WeatherBot
{
    class Program
    {
        private static string token { get; set; } = "2129202672:AAHE0dyF-Pu2cnSc5zE1O3NlnDWAZj5M1-0";
        private static TelegramBotClient client;

        static string NameCity; // название города
        static float tempOfCity; //температура
        static string nameOfCity; //название города для вывода
       
        static float FeelLike; // ощущается как
        static string Wind_Speed; // скорость ветра
        static int TimeDate;
        public static void Main(string[] args)
        {
            client = new TelegramBotClient(token) { Timeout = TimeSpan.FromSeconds(10)};
            client.OnMessage += Bot_OnMessage;
            client.StartReceiving();
            Console.ReadKey();
            client.StopReceiving();
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
{
                var message = e.Message;
                string b = message.Text;
                string[] a = b.Split(' ');
                if (a[0] == "/weather")
                {
                
                if (message.Type == MessageType.Text)
                {
                NameCity = a[1];
                Weather(NameCity);
                
                await client.SendTextMessageAsync(message.Chat.Id, $"Статистика по городу {nameOfCity} на {Data(TimeDate)}\nТемпература: {Math.Round(tempOfCity)} °C \n Ощущается как: {Math.Round(FeelLike,1)} °C\n Скорость ветра: {Wind_Speed}");              
                }
}
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
                FeelLike = weatherResponse.Main.Feels_like -273;
                Wind_Speed = weatherResponse.Wind.Speed;
                TimeDate = weatherResponse.DT;
        }
        public static string Data(int TimeDate)
        {
            DateTime data = new DateTime (1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            data = data.AddSeconds(TimeDate);
            data = data.ToLocalTime();
            return data.ToString("dd-MM-yyyy \nВремя: hh:mm:ss");
        }

        
    }
}