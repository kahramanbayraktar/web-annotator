using AnnotationApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AnnotationApi.Services
{
    public class AnnotationService : IAnnotationService
    {
        private readonly IMongoCollection<IAnnotation> _annotations;

        public AnnotationService(IAnnotationDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _annotations = database.GetCollection<IAnnotation>(settings.AnnotationsCollectionName);
        }

        public List<IAnnotation> Get()
        {
            var annotations = _annotations.Find(r => true).ToList();
            return annotations;
        }

        public IAnnotation Get(string id) =>
            _annotations.Find(annotation => annotation.DbId == id).FirstOrDefault();

        public IAnnotation GetByAnnotation(string id) =>
            _annotations.Find(annotation => annotation.Id == id).FirstOrDefault();

        public List<IAnnotation> GetByTarget(string id) =>
            _annotations.Find(annotation => annotation.Target.Id.StartsWith(id)).ToList();

        public List<IAnnotation> Search(string text) //, string startDate, string endDate)
        {
            text = text.Trim().ToLower();

            //DateTime.TryParse(startDate, out var start);
            //DateTime.TryParse(endDate, out var end);

            return _annotations.Find(annotation =>
                annotation.Creator.Name.ToLower().Contains(text)
                || annotation.Body.ToLower().Contains(text)
                //|| start != null && annotation.Created >= start && end != null && annotation.Created <= end
            ).ToList();
        }

        public IAnnotation Create(IAnnotation annotation)
        {
            _annotations.InsertOne(annotation);
            return annotation;
        }

        public void Update(string id, IAnnotation annotationIn) =>
            _annotations.ReplaceOne(annotation => annotation.DbId == id, annotationIn);

        public void Remove(IAnnotation annotationIn) =>
            _annotations.DeleteOne(annotation => annotation.DbId == annotationIn.Id);

        public void Remove(string id) =>
            _annotations.DeleteOne(annotation => annotation.DbId == id);
    }
}
