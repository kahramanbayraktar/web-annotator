using System;
using System.Text.Json.Serialization;

namespace AnnotationApi.Models
{
    public class Search
    {
        /*
        "text": "some search text",
        "start": "2017-11-28T00:00:00.000000+00:00",
        "end": "2017-11-29T00:00:00.000000+00:00"
        */

        [JsonPropertyName("text")]
        public string Text { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}
