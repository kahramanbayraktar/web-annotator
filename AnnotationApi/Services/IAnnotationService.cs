using AnnotationApi.Models;
using System.Collections.Generic;

namespace AnnotationApi.Services
{
    public interface IAnnotationService
    {
        List<IAnnotation> Get();
        IAnnotation Get(string id);
        IAnnotation GetByAnnotation(string id);
        List<IAnnotation> GetByTarget(string id);
        List<IAnnotation> Search(string text); //, string startDate, string endDate)
        IAnnotation Create(IAnnotation annotation);
        void Update(string id, IAnnotation annotationIn);
        void Remove(IAnnotation annotationIn);
        void Remove(string id);
    }
}