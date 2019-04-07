using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace App.WhoAmI
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhoAmIController : ControllerBase
    {
        /// <summary>
        ///     Echo the current request headers and include information about the
        ///     current webserver host
        /// </summary>
        [HttpGet]
        public WhoAmIModel Get() => new WhoAmIModel {Host = Dns.GetHostName(), RequestHeaders = Request.Headers};
    }
}