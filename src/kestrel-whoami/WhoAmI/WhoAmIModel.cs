using Microsoft.AspNetCore.Http;

namespace App.WhoAmI
{
    public class WhoAmIModel
    {
        public string Host { get; set; }
        public IHeaderDictionary RequestHeaders { get; set; }
    }
}