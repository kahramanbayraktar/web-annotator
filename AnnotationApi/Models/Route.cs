using AnnotationApi.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnnotationApi.Models
{
    public class Route
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("airline")]
        public Airline Airline { get; set; }

        [BsonSerializer(typeof(BsonStringNumericSerializer))]
        [BsonElement("src_airport")]
        public string Departure { get; set; }

        [BsonSerializer(typeof(BsonStringNumericSerializer))]
        [BsonElement("dst_airport")]
        public string DstAirport { get; set; }

        [BsonElement("codeshare")]
        public string CodeShare { get; set; }

        [BsonElement("stops")]
        public int Stops { get; set; }

        [BsonSerializer(typeof(BsonStringNumericSerializer))]
        [BsonElement("airplane")]
        public string Airplane { get; set; }
    }

    [BsonNoId]
    public class Airline
    {
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("alias")]
        public string Alias { get; set; }

        [BsonElement("iata")]
        public string Iata { get; set; }
    }
}
