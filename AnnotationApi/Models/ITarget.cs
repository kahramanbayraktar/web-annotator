namespace AnnotationApi.Models
{
    public interface ITarget
    {
        string Id { get; set; }
        string Type { get; set; }
        string Format { get; set; }
    }
}