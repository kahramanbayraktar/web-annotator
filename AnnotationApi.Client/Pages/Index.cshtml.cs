using AnnotationApi.Client.Models;
using JsonLD.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnnotationApi.Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IndexModel> _logger;
        private readonly string _endPoint;

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _endPoint = _configuration.GetSection("AnnotatorApi:EndPoint").Value;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAnnotation([FromForm] ClientAnnotation form)
        {
            var annotation = new Annotation
            {
                Context = "http://www.w3.org/ns/anno.jsonld",
                Id = null,
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
