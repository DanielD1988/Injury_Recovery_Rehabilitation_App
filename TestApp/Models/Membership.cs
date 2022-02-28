using Newtonsoft.Json;

namespace TestApp.Models
{
    public class Membership
    {
        [JsonProperty("membershipExpiryDate")]
        public string MembershipDate { get; set; }
    }
}
