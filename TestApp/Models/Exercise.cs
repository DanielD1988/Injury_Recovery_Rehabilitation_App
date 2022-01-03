using Newtonsoft.Json;


namespace TestApp.Models
{
    /// <summary>
    /// This describes the data taken from the exercise Json Structure
    /// </summary>
    public class Exercise
    {
        [JsonProperty("name")]
        public string ExerciseName { get; set; }
        [JsonProperty("description")]
        public string ExerciseDescription { get; set; }
        [JsonProperty("videoLink")]
        public string VideoLink { get; set; }
    }
}
