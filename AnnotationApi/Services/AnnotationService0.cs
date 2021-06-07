using AnnotationApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AnnotationApi.Services
{
    public class AnnotationService0
    {
        private readonly IMongoCollection<Annotation> _annotations;

        public AnnotationService0(IAnnotationDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _annotations = database.GetCollection<Annotation>(settings.AnnotationsCollectionName);
        }

        //public List<Annotation> Get() =>
        //    _annotations.Find(annotation => true).ToList();

        public List<Annotation> Get()
        {
            var annotations = _annotations.Find(r => true).ToList();
            return annotations;
        }

        public Annotation Get(string id) =>
            _annotations.Find<Annotation>(annotation => annotation.Id == id).FirstOrDefault();

        public Annotation Create(Annotation annotation)
        {
            _annotations.InsertOne(annotation);
            return annotation;
        }

        public void Update(string id, Annotation annotationIn) =>
            _annotations.ReplaceOne(annotation => annotation.Id == id, annotationIn);

        public void Remove(Annotation annotationIn) =>
            _annotations.DeleteOne(annotation => annotation.Id == annotationIn.Id);

        public void Remove(string id) =>
            _annotations.DeleteOne(annotation => annotation.Id == id);
    }
}
