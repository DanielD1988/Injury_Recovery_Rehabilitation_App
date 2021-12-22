﻿using Newtonsoft.Json;
using System;
using System.IO;

namespace TestApp.models
{
    /// <summary>
    /// This ExercisePlan class sets data comming from the FirebaseMethods. The [JsonProperty()] decorator is used to
    /// deserialize key value pair JSON structure so the value can be extracted to the variable.
    /// There is also a base64 string to image conversion
    /// </summary>
    public class ExercisePlan // https://stackoverflow.com/questions/38743280/deserialize-json-object-xamarin-android-c-sharp
    {
        private string base64stringToImage;
        private Xamarin.Forms.ImageSource image;
        [JsonProperty("name")]
        public string exerciseName { get; set; }
        [JsonProperty("desc")]
        public string exerciseDescription { get; set; }
        [JsonProperty("exerciseInfo")]
        public string exerciseInfo { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("exercise1")]
        public string Exercise1 { get; set; }
        [JsonProperty("exercise2")]
        public string Exercise2 { get; set; }
        [JsonProperty("exercise3")]
        public string Exercise3 { get; set; }
        public string exerciseListKey { get; set; }
        [JsonProperty("exerciseImage")]
        //https://blog.mzikmund.com/2018/01/displaying-base64-encoded-image-in-xamarin-forms/
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
        }///////////////////////////////////////////////////////////////////
    }
}