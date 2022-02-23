using Newtonsoft.Json;

namespace TestApp.Models
{
    public class PatientList
    {
        [JsonProperty("patientUid")]
        public string PatientUid { get; set; }
        [JsonProperty("patientName")]
        public string PatientName { get; set; }
    }
}
