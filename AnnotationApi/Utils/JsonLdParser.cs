using AnnotationApi.Models;
using System.Text.Json;

namespace AnnotationApi.Utils
{
    public class JsonLdParser
    {
        public Route Deserialize(string jsonLd)
        {
            var route = JsonSerializer.Deserialize<Route>(jsonLd);
            return route;
        }
    }
}
