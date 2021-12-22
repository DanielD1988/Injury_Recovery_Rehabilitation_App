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
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("injuryType")]
        public string InjuryType { get; set; }
        [JsonProperty("occurred")]
        public string InjuryOccurred { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("severity")]
        public int InjurySeverity { get; set; }
        [JsonProperty("start")]
        public DateFormatHandling StartDate { get; set; }
        [JsonProperty("end")]
        public DateFormatHandling EndDate { get; set; }
    }
}
