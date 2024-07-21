using Newtonsoft.Json;

namespace Core.Models.Slack
{
    public class SlackPayload
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
