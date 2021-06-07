using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnnotationApi.Models
{
    public class Annotation
    {
        /*
        "@context": "http://www.w3.org/ns/anno.jsonld",
        "id": "http://example.org/anno2",
        "type": "Annotation",
        */

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DbId { get; set; }

        [JsonPropertyName("@context")]
        [BsonElement("@context")]
        public string Context { get; set; }

        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [JsonPropertyName("@type")]
        [BsonElement("type")]
        public string Type { get; set; }

        //[BsonElement("body")]
        //public Body Body { get; set; }

        [JsonPropertyName("body")]
        [BsonElement("body")]
        public string Body { get; set; }

        [BsonElement("target")]
        public Target Target { get; set; }
    }

    [BsonNoId]
    public class Body
    {
        /*
        "body": {
            "id": "http://example.org/analysis1.mp3",
            "format": "audio/mpeg",
            "language": "fr"
          },
        */
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("format")]
        public string Format { get; set; }

        [BsonElement("language")]
        public string Language { get; set; }
    }

    [BsonNoId]
    public class Target
    {
        /*
        "target": {
            "id": "http://example.gov/patent1.pdf",
            "format": "application/pdf",
            "language": ["en", "ar"],
            "textDirection": "ltr",
            "processingLanguage": "en"
        }
        */

        [JsonPropertyName("@id")]
        [BsonElement("id")]
        public string Id { get; set; }

        [JsonPropertyName("@type")]
        [BsonElement("type")]
        public string Type { get; set; }

        [JsonPropertyName("format")]
        [BsonElement("format")]
        public string Format { get; set; }

        //[BsonElement("language")]
        //public string Language { get; set; }

        //[BsonElement("textDirection")]
        //public string TextDirection { get; set; }

        //[BsonElement("processingLanguage")]
        //public string ProcessingLanguage { get; set; }
    }
}
