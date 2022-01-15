using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// This describes the data for the progress Json Structure
    /// </summary>
    public class Progress
    {
        [JsonProperty("complete")]
        public bool isComplete { get; set; }
    }
}
