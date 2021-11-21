using Newtonsoft.Json;
using System;
using System.IO;

namespace TestApp.models
{
    public class Exercise
    {
        private string base64stringToImage;
        private Xamarin.Forms.ImageSource image;
        [JsonProperty("name")]
        public string exerciseName { get; set; }
        [JsonProperty("desc")]
        public string exerciseDescription { get; set; }
        [JsonProperty("exerciseInfo")]
        public string exerciseInfo { get; set; }
        public string exerciseListKey { get; set; }
        [JsonProperty("exerciseImage")]
        public string ImageBase64
        {
            get { return base64stringToImage; }
            set
            {
                base64stringToImage = value;
                Image = Xamarin.Forms.ImageSource.FromStream(
                    () => new MemoryStream(Convert.FromBase64String(base64stringToImage)));
            }
        }
        public Xamarin.Forms.ImageSource Image
        {
            get { return image; }
            set
            {
                image = value; 
            }
        }
    }
}
