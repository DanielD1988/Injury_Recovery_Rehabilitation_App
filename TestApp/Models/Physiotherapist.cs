using Newtonsoft.Json;
using System;
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
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("physioIdNumber")]
        public string IdNumber { get; set; }
        [JsonProperty("patients")]
        public string PatientUid { get; set; }
        [JsonProperty("membershipExpiryDate")]
        public string Membership { get; set; }
    }
}
