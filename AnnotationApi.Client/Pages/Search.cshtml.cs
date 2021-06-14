using AnnotationApi.Client.Models;
using JsonLD.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnnotationApi.Client.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SearchModel> _logger;
        private readonly string _endPoint;
        private readonly string _clientRootUrl;

        public List<Analysis> Analyses;

        public SearchModel(IConfiguration configuration, ILogger<SearchModel> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _endPoint = _configuration.GetSection("AnnotatorApi:EndPoint").Value;
            _clientRootUrl = _configuration.GetSection("ClientRootUrl").Value;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostSearch([FromForm] Search form)
        {
            var search = new Search
            {
                Text = form.Text
            };

            var contextProvider = new StaticContextProvider();
            var serializer = new EntitySerializer(contextProvider);

            try
            {
                dynamic jsonLd = serializer.Serialize(search);
                var jsonLdStr = jsonLd.ToString();

                var client = new HttpClient();
                var content = new StringContent(jsonLdStr, Encoding.UTF8, "application/json");

                using var response = await client.PostAsync($"{_endPoint}/annotation/search", content);
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
