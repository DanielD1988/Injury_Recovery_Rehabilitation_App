using Newtonsoft.Json;
using System;

namespace TestApp.Models
{
    /// <summary>
    /// This describes the data taken from the patient Json Structure
    /// </summary>
    class Patient
    {
        [JsonProperty("name")]
        public string PatientName { get; set; }
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
        public DateTime StartDate { get; set; }
        [JsonProperty("end")]
        public DateTime EndDate { get; set; }
        [JsonProperty("plan")]
        public string ExerPlan { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
