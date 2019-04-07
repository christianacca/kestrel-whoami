using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;

namespace App.KestrelSettings
{
    public class KestrelSettingsSetup
    {
        public static void Configure(WebHostBuilderContext builderContext, KestrelServerOptions options)
        {
            // Configuring Limits from appsettings.json is not supported.
            // So we manually copy them from config.
            // See https://github.com/aspnet/KestrelHttpServer/issues/2216
            var limits = builderContext.Configuration.GetSection("Kestrel").Get<KestrelServerOptions>()?.Limits;
            if (limits == null)
            {
                return;
            }

            foreach (var property in typeof(KestrelServerLimits).GetProperties().Where(p => p.CanWrite))
            {
                var value = property.GetValue(limits);
                property.SetValue(options.Limits, value);
            }
        }
    }
}