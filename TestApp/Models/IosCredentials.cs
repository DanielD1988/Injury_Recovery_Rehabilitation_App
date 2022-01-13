using Newtonsoft.Json;

namespace TestApp.Models
{
    public class IosCredentials
    {
        [JsonProperty("uid")]
        public string userId { get; set; }
        [JsonProperty("salt")]
        public string salt { get; set; }
        [JsonProperty("md5")]
        public string saltHashed { get; set; }
    }
}
