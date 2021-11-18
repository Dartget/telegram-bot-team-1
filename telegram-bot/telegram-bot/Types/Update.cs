using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TelegramBot.Types
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Update
    {
        [JsonProperty("update_id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Message Message { get; set; }
    }
}
