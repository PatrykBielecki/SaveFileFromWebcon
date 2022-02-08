using System.Collections.Generic;
using Newtonsoft.Json;

namespace RobotKan.DTO
{
    public partial class WebconElement
    {
        [JsonProperty("attachments")]
        public List<ElementAttachment> Attachments { get; set; }
    }

    public partial class ElementAttachment
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}

