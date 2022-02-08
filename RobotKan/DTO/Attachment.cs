using Newtonsoft.Json;

namespace RobotKan.DTO
{
    public partial class Attachment
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}

