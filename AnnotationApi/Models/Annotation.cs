using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace AnnotationApi.Models
{
    public class Annotation
    {
        /*
        "@context": "http://www.w3.org/ns/anno.jsonld",
        "id": "https://annotator.api/a/60ba011adf1412fe8345a899", // https://annotatorapi.azurewebsites.net/annotation/get/60ba011adf1412fe8345a899
        "type": "Annotation",
        "body": "This built-in trigger sends an HTTP request to a URL for a Swagger file that describes a REST API and returns a response that contains that file's content.",
        "created": "2017-11-28T18:56:04.889815+00:00"
        */

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string DbId { get; set; }

        [JsonPropertyName("@context")]
        [BsonElement("@context")]
        public string Context { get; set; }

        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("body")]
        public string Body { get; set; }

        [BsonElement("target")]
        public Target Target { get; set; }

        [BsonElement("creator")]
        public Creator Creator { get; set; }

        [BsonElement("created")]
        public DateTime Created { get; set; }
    }

    [BsonNoId]
    public class Creator
    {
        /*
        "creator": {
            "id": "http://example.org/user1",
            "name": "Anne O'Tater",
            "nick": "Ann0"
          }
        */

        [JsonPropertyName("id")]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("nick")]
        public string Nick { get; set; }
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
