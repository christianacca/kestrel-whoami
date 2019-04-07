using System.Net;

namespace App.Health
{
    public class HealthCheckToggle
    {
        public bool Healthy { get; set; } = true;

        public string Host { get; set; } = Dns.GetHostName();

        public void Toggle() 
        {
            Healthy = !Healthy;
        }
    }
}