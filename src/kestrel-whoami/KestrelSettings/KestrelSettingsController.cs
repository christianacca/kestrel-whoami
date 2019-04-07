using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

namespace App.KestrelSettings
{
    [Route("api/[controller]")]
    [ApiController]
    public class KestrelSettingsController : ControllerBase
    {
        public KestrelSettingsController(IOptions<KestrelServerOptions> settings) 
        {
            Settings = settings.Value;
        }

        private KestrelServerOptions Settings { get; }

        
        /// <summary>
        /// Get the Kestrel webserver settings that has been configured
        /// </summary>
        [HttpGet]
        public KestrelServerLimits Get() => Settings.Limits;
    }
}
