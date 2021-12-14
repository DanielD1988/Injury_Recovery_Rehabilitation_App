using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// This describes the data taken from the patient Json Structure
    /// </summary>
    class Patient
    {
        [JsonProperty("name")]
        public string PatientName { get; set; }
        [JsonProperty("number")]
        public string Phone { get; set; }
        [JsonProperty("sex")]
        public string Sex { get; set; }
        [JsonProperty("injuryType")]
        public string InjuryType { get; set; }
        [JsonProperty("age")]
        public string Age { get; set; }
    }
}
