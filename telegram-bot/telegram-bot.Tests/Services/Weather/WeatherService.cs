using Xunit;

namespace TelegramBotTests
{
	public class WeatherService
    {
		[Fact]
		public void TestMethod1()
        {
            string[] message = new string[] { "/getweather", "Moscow" };
            string city = TelegramBot.Services.Weather.WeatherService.OneOrMore(message);

            Assert.Equal(message[1], city);
        }
		[Fact]
		public void TestMethod2()
        {
            string[] message = new string[] { "/getweather", "Нижний","Новгород" };
            string city1 = TelegramBot.Services.Weather.WeatherService.OneOrMore(message);

            Assert.Equal(message[1]+" "+message[2], city1);
        }
		[Fact]
		public void TestMethod3()
        {
            string[] message = new string[] { "/getweather", "1", "2","3" };
            string city2 = TelegramBot.Services.Weather.WeatherService.OneOrMore(message);

            Assert.Equal(message[1] + " " + message[2]+ " " + message[3], city2);
        }
    }
}
