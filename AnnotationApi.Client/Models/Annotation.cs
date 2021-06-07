namespace AnnotationApi.Client.Models
{
    public class Annotation
    {
        public string DbId { get; set; }
        public string Context { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Body { get; set; }
        public Target Target { get; set; }
    }

    public class Body
    {
        public int Id { get; set; }
        public string Format { get; set; }
        public string Language { get; set; }
    }

    public class Target
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
    }
}
