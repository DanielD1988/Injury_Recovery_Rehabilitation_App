using MediaManager;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This class displays details about the exercise including playing a video
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowExerciseContent : ContentPage
    {
        string downloadLink = "";
        string name = "";
        string copyright = "";
        string uid = "";
        bool exerciseComplete = false;
        public ShowExerciseContent(string downloadLink,string exerciseName,string videoCopyright,string uid)
        {
            this.downloadLink = downloadLink;
            name = exerciseName;
            copyright = videoCopyright;
            this.uid = uid;
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            exerciseLabel.Text = name;
            copyRightLabel.Text = copyright;
        }

        private async void showVideo(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Play(downloadLink);
        }
        private async void pauseVideo(object sender, EventArgs e)
        {
           await CrossMediaManager.Current.PlayPause();
        }
        private async void endVideo(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();
        }
        void exercise(object sender, EventArgs e)
        {
            exerciseComplete = true;
            exer.IsEnabled = false;
        }
        private async void completed(object sender, EventArgs e)
        {
            if(exerciseComplete == true)
            {

            }
        }
        private async void returned(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }
}