using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace App.Swagger
{
    public class SwaggerUIOptionsSetup: IConfigureOptions<SwaggerUIOptions>
    {
        public void Configure(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/LibraryOpenAPISpecification/swagger.json", "ASP.Net Core WhoAmI service");
            options.RoutePrefix = "";
        }
    }
}