using Microsoft.AspNetCore.Mvc;

namespace App.Health
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthToggleController : ControllerBase
    {
        public HealthToggleController(HealthCheckToggle healthCheck) 
        {
            HealthCheck = healthCheck;
        }

        private HealthCheckToggle HealthCheck { get; }

        /// <summary>
        /// Toggle the healthy state of this webserver instance
        /// </summary>
        [HttpGet]
        public HealthCheckToggle Get()
        {
            HealthCheck.Toggle();
            return HealthCheck;
        }
    }
}
