using Microsoft.VisualStudio.TestTools.UnitTesting;
using TelegramBot.Services.Weather;
namespace Weather_test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] message = new string[] { "/GetWeather", "Moscow" };
            string city = WeatherService.OneOrMore(message);

            Assert.AreEqual(message[1], city);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string[] message = new string[] { "/GetWeather", "Нижний","Новгород" };
            string city1 = WeatherService.OneOrMore(message);

            Assert.AreEqual(message[1]+" "+message[2], city1);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string[] message = new string[] { "/GetWeather", "1", "2","3" };
            string city2 = WeatherService.OneOrMore(message);

            Assert.AreEqual(message[1] + " " + message[2]+ " " + message[3], city2);
        }
    }
}
