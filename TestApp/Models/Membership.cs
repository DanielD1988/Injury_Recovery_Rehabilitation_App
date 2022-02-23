using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Models
{
    public class Membership
    {
        [JsonProperty("membershipExpiryDate")]
        public string MembershipDate { get; set; }
    }
}
