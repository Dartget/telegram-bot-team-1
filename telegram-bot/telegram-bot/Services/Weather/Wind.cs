using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TelegramBot.Services.Weather
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Wind
    {
        [JsonProperty(Required = Required.Always)]
        public string Speed { get; set; }
    }
}
