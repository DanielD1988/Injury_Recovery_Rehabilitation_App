using Newtonsoft.Json;

namespace TestApp.Models
{
    public class Progress
    {
        [JsonProperty("complete")]
        public bool isComplete { get; set; }
    }
}
