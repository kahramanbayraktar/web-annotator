using AnnotationApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AnnotationApi.Services
{
    public class AnnotationService
    {
        private readonly IMongoCollection<Annotation> _annotations;

        public AnnotationService(IAnnotationDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _annotations = database.GetCollection<Annotation>(settings.AnnotationsCollectionName);
        }

        public List<Annotation> Get()
        {
            var annotations = _annotations.Find(r => true).ToList();
            return annotations;
        }

        public Annotation Get(string id) =>
            _annotations.Find(annotation => annotation.DbId == id).FirstOrDefault();

        public Annotation GetByAnnotation(string id) =>
            _annotations.Find(annotation => annotation.Id == id).FirstOrDefault();

        public List<Annotation> GetByTarget(string id) =>
            _annotations.Find(annotation => annotation.Target.Id.StartsWith(id)).ToList();

        public Annotation Create(Annotation annotation)
        {
            _annotations.InsertOne(annotation);
            return annotation;
        }

        public void Update(string id, Annotation annotationIn) =>
            _annotations.ReplaceOne(annotation => annotation.DbId == id, annotationIn);

        public void Remove(Annotation annotationIn) =>
            _annotations.DeleteOne(annotation => annotation.DbId == annotationIn.Id);

        public void Remove(string id) =>
            _annotations.DeleteOne(annotation => annotation.DbId == id);
    }
}
