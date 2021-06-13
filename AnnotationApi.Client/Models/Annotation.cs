using Newtonsoft.Json;

namespace AnnotationApi.Client.Models
{
    public class Annotation
    {
        public string DbId { get; set; }
        [JsonProperty("@context")]
        public string Context { get; set; }
        [JsonProperty("@id")]
        public string Id { get; set; }
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string Body { get; set; }
        public Target Target { get; set; }
        public Creator Creator { get; set; }
    }

    public class Creator
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
    }

    public class Body
    {
        public int Id { get; set; }
        public string Format { get; set; }
        public string Language { get; set; }
    }

    public class Target
    {
        [JsonProperty("@id")]
        public string Id { get; set; }
        [JsonProperty("@type")]
        public string Type { get; set; }
        public string Format { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
    }
}
