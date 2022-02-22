using Newtonsoft.Json;

namespace TestApp.Models
{
    public class Encryption
    {
        [JsonProperty("patientUid")]
        public string PatientUid { get; set; }
        [JsonProperty("encryptionKey")]
        public string EncryptionKey { get; set; }
    }
}
