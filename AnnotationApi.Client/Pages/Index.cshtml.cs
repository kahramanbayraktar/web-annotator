using AnnotationApi.Client.Models;
using JsonLD.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AnnotationApi.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IndexModel> _logger;
        private readonly string _endPoint;

        public List<Annotation> Annotations = new List<Annotation>();
        public Annotation SelectedAnnot;

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _endPoint = _configuration.GetSection("AnnotatorApi:EndPoint").Value;
        }

        public void OnGet(string annotId)
        {
            var client = new HttpClient();

            var id = "https"; // TODO

            using var response = client.GetStringAsync($"{_endPoint}/annotation/getbytarget/{HttpUtility.UrlEncode(id)}");
            var apiResponse = response.Result;

            var annots = JsonConvert.DeserializeObject<List<Annotation>>(apiResponse);

            foreach (var annotation in annots)
            {
                var dimsStart = annotation.Target.Id.IndexOf("=", StringComparison.InvariantCultureIgnoreCase);
                var dimensions = annotation.Target.Id.Substring(dimsStart + 1).Split(",");
                annotation.Target.X = Convert.ToInt32(dimensions[0]);
                annotation.Target.Y = Convert.ToInt32(dimensions[1]);
                annotation.Target.W = Convert.ToInt32(dimensions[2]);
                annotation.Target.H = Convert.ToInt32(dimensions[3]);
            }

            this.Annotations = annots;

            if (annotId != null)
            {
                this.SelectedAnnot = this.Annotations.SingleOrDefault(x => x.DbId == annotId);
            }
        }

        public async Task<IActionResult> OnPostAnnotation([FromForm] ClientAnnotation form)
        {
            var annotation = new Annotation
            {
                Context = "http://www.w3.org/ns/anno.jsonld",
                Id = $"http://team3analyzer.com/analysis/{Guid.NewGuid()}",
                Type = "Annotation",
                Body = form.Text,
                Target = new Target
                {
                    Id = form.TargetId + $"?xywh={form.X},{form.Y},{form.W},{form.H}",
                    Type = "Image",
                    Format = "image/jpeg"
                }
            };

            var contextProvider = new StaticContextProvider();
            var serializer = new EntitySerializer(contextProvider);

            try
            {
                dynamic jsonLd = serializer.Serialize(annotation);
                var jsonLdStr = jsonLd.ToString();

                var client = new HttpClient();
                var content = new StringContent(jsonLdStr, Encoding.UTF8, "application/json");

                using var response = await client.PostAsync($"{_endPoint}/annotation/create", content);
                var apiResponse = await response.Content.ReadAsStringAsync();
                return new JsonResult(apiResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
