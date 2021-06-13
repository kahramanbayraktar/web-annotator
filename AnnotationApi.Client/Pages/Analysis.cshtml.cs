using AnnotationApi.Client.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AnnotationApi.Client.Pages
{
    public class AnalysisModel : PageModel
    {
        private readonly ILogger<AnalysisModel> _logger;

        public List<Analysis> Analyses;

        public AnalysisModel(ILogger<AnalysisModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            this.Analyses = new List<Analysis>
            {
                new Analysis {DbId = "60be725a980f947a351e2e97", Caption = "First analysis"},
                new Analysis {DbId = "60be735c980f947a351e2e98", Caption = "Analysis 2"},
                new Analysis {DbId = "60be99c4980f947a351e2e99", Caption = "Analysis 3"}
            };
        }
    }
}
