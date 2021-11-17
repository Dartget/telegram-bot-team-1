using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TelegramBot.Services.Weather
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class WeatherResponse
    {
        [JsonProperty(Required = Required.Always)]
        public TemperatureInfo Main { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Wind Wind {get;set;}

        [JsonProperty(Required = Required.Always)]
        public int DT{get;set;}

    }
}
