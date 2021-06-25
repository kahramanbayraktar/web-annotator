using MongoDB.Bson.Serialization.Attributes;

namespace AnnotationApi.Models
{
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
}