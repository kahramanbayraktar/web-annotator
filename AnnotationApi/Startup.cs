using AnnotationApi.Models;
using AnnotationApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AnnotationApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<AnnotationDatabaseSettings>(
                Configuration.GetSection(nameof(AnnotationDatabaseSettings)));
            services.Configure<FlightDatabaseSettings>(
                Configuration.GetSection(nameof(FlightDatabaseSettings)));

            services.AddSingleton<IAnnotationDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AnnotationDatabaseSettings>>().Value);
            services.AddSingleton<IFlightDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<FlightDatabaseSettings>>().Value);

            services.AddSingleton<AnnotationService>();
            services.AddSingleton<RouteService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
