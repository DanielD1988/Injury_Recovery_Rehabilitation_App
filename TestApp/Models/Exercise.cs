using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Converters;
namespace TestApp.models
{
    public class Exercise
    {
        [JsonProperty("name")]
        public string exerciseName { get; set; }
        [JsonProperty("desc")]
        public string exerciseDescription { get; set; }
        //public string exerciseKey { get; set; }
    }
    
}
