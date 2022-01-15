using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestApp.Models
{
    /// <summary>
    /// This describes the data for the physiotherapist Json Structure
    /// </summary>
    class Physiotherapist
    {
        [JsonProperty("name")]
        public string PhysioName { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("regNumber")]
        public string RegNumber { get; set; }
        [JsonProperty("patientUid")]
        public string PatientUid { get; set; }

        //extra fields for mock database
        public List<string> patientList = new List<string>();
        public string physioUid = "";
    }
}
