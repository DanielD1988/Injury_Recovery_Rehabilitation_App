using Newtonsoft.Json;

namespace TestApp.Models
{
    /// <summary>
    /// This describes the data for the patient Json Structure
    /// </summary>
    public class Patient
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
        [JsonProperty("exercise1")]
        public string Exer1 { get; set; }
        [JsonProperty("exercise2")]
        public string Exer2 { get; set; }
        [JsonProperty("exercise3")]
        public string Exer3 { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
