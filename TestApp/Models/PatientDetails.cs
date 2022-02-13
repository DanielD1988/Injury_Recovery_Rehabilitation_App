using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// This database model is used retrieve the patient name and patient id
    /// </summary>
    public class PatientDetails
    {
        [JsonProperty("patientName")]
        public string Name { get; set; }
        [JsonProperty("patients")]
        public string Uid { get; set; }
    }
}
