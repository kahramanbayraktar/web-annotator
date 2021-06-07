namespace AnnotationApi.Models
{
    public class AnnotationDatabaseSettings : IAnnotationDatabaseSettings
    {
        public string AnnotationsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IAnnotationDatabaseSettings
    {
        string AnnotationsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
