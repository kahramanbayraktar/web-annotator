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
        private readonly string _clientRootUrl;

        public List<Annotation> Annotations = new List<Annotation>();
        public Annotation SelectedAnnot;
        public string TargetId;
        public string ImageUrl;

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _endPoint = _configuration.GetSection("AnnotatorApi:EndPoint").Value;
            _clientRootUrl = _configuration.GetSection("ClientRootUrl").Value;
        }

        public void OnGet(string id, string annotId)
        {
            // TODO: duplicate
            var analyses = new List<Analysis>
            {
                new Analysis
                {
                    DbId = "60be725a980f947a351e2e97", Caption = "First analysis",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/7/77/Avatar_cat.png"
                },
                new Analysis
                {
                    DbId = "60be735c980f947a351e2e98", Caption = "Analysis 2",
                    Image = "https://www.researchgate.net/profile/Atsushi-Komine/publication/327499076/figure/fig2/AS:668140845412360@1536308599439/Co-occurrence-network-analysis-sub-graph-the-GT.png"
                }
                ,
                new Analysis
                {
                    DbId = "60be99c4980f947a351e2e99", Caption = "Analysis 3",
                    Image = "https://media-exp1.licdn.com/dms/image/C5612AQFCtWLPTYN6Ag/article-cover_image-shrink_600_2000/0/1520128664426?e=1627516800&v=beta&t=cYAZMcRm6lk_KYuI9M4RZP6YEvyYsSf1S2QU-obby5I"
                }
            };
            var analysis = analyses.SingleOrDefault(x => x.DbId == id);

            if (analysis == null)
                return;

            this.ImageUrl = analysis.Image;
            
            var client = new HttpClient();

            var targetId = $"{_clientRootUrl}/analysis/{id}";
            var encodedUrl = $"{_endPoint}/annotation/getbytarget?id={HttpUtility.UrlEncode(targetId)}";

            using var response = client.GetStringAsync(encodedUrl);
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

            this.TargetId = targetId;
            this.Annotations = annots;

            if (annotId != null)
            {
                this.SelectedAnnot = this.Annotations.SingleOrDefault(x => x.DbId == annotId);
            }
        }

        public async Task<IActionResult> OnPostAnnotation([FromForm] ClientAnnotation form)
        {
            var targetId = $"{form.TargetId}?xywh={form.X},{form.Y},{form.W},{form.H}";

            var annotation = new Annotation
            {
                Context = "http://www.w3.org/ns/anno.jsonld",
                Id = null, // this will be populated on the annotator server
                Type = "Annotation",
                Body = form.Text,
                Target = new Target
                {
                    Id = targetId,
                    Type = "Image",
                    Format = "image/jpeg"
                },
                Creator = new Creator
                {
                    Id = "https://analyzer.app/user/karr",
                    Name = "Kahraman Bayraktar",
                    Nick = "karr"
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
