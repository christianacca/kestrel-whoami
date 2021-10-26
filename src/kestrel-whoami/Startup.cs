using App.Health;
using App.Swagger;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services
                .AddProblemDetails()
                .AddControllersWithViews()
                .AddJsonOptions(o => o.JsonSerializerOptions.IgnoreNullValues = true)
                .AddProblemDetailsConventions();

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
        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseEndpoints(endpoints => {
                endpoints.MapHealthChecks("/api/health");
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
