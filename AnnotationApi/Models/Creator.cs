using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace AnnotationApi.Models
{
    [BsonNoId]
    public class Creator : ICreator
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
}