using App.Health;
using App.Swagger;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        ///     Add services to the DI container.
        /// </summary>
        /// <remarks>
        ///     This method gets called by the runtime
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<MvcJsonOptions>(options =>
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);

            services.AddProblemDetails();

            services.AddSingleton<HealthCheckToggle>();
            services.ConfigureOptions<HealthCheckOptionsSetup>();
            services.AddHealthChecks()
                .AddCheck<ToggledHealthCheck>(nameof(ToggledHealthCheck));

            services.ConfigureOptions<SwaggerGenOptionsSetup>();
            services.ConfigureOptions<SwaggerUIOptionsSetup>();
            services.AddSwaggerGen();

        }

        /// <summary>
        ///     Configures the HTTP request pipeline
        /// </summary>
        /// <remarks>
        ///     This method gets called by the runtime
        /// </remarks>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseProblemDetails();

            app.UseHealthChecks("/api/health");

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMvc();
        }
    }
}
