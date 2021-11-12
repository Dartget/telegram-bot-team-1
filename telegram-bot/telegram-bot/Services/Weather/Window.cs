using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace telegram_bot.Services.Weather
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Window
    {
        [JsonProperty(Required = Required.Always)]
        public string Speed { get; set; }
    }
}
