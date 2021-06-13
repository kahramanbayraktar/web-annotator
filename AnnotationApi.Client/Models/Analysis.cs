using Newtonsoft.Json;

namespace AnnotationApi.Client.Models
{
    public class Analysis
    {
        public string DbId { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
