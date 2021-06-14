using Newtonsoft.Json;

namespace AnnotationApi.Client.Models
{
    public class Search
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
