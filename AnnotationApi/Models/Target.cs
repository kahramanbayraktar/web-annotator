using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace AnnotationApi.Models
{
    [BsonNoId]
    public class Target : ITarget
    {
        /*
        "target": {
            "id": "https://analysis.app/analysis/1?xywh=39,18,172,96",
            "type": "Image",
            "format": "image/jpeg"
        }
        */

        [JsonPropertyName("id")]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("format")]
        public string Format { get; set; }
    }
}