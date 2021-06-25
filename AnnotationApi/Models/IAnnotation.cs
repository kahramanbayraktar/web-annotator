using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace AnnotationApi.Models
{
    [BsonSerializer(typeof(ImpliedImplementationInterfaceSerializer<IAnnotation, Annotation>))]
    public interface IAnnotation
    {
        string DbId { get; set; }
        string Context { get; set; }
        string Id { get; set; }
        string Type { get; set; }
        string Body { get; set; }
        Target Target { get; set; }
        Creator Creator { get; set; }
        DateTime Created { get; set; }
    }
}