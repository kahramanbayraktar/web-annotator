using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace AnnotationApi.Models
{
    public class Annotation : IAnnotation
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
}
