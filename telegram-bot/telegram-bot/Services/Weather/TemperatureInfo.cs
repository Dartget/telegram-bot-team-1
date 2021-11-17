using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TelegramBot.Services.Weather
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class TemperatureInfo
    {
        [JsonProperty(Required = Required.Always)]
        public float Temp { get; set; }
        [JsonProperty(Required = Required.Always)]
        public float Feels_like { get; set; }
    }
}
