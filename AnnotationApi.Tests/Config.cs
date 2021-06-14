using Microsoft.Extensions.Configuration;
using System.IO;

namespace AnnotationApi.Tests
{
    public static class Config
    {
        private static readonly IConfiguration Configuration;

        static Config()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public static string Get(string name)
        {
            string appSettings = Configuration[name];
            return appSettings;
        }
    }
}
