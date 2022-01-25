using MediaManager;
using System;
using TestApp.ViewModels;
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
        int whichExercise = 0;
        int min = 0;
        int max = 0;
        bool exerciseComplete = false;
        DisplayPatientExercisePlanViewModel model;
        /// <summary>
        /// This constructor takes a downloadlink to an exercise video the copyright to the owner of the video 
        /// the currently logged in patient user id and the shared view model instance
        /// </summary>
        /// <param name="downloadLink"></param>
        /// <param name="exerciseName"></param>
        /// <param name="videoCopyright"></param>
        /// <param name="uid"></param>
        /// <param name="viewModel"></param>
        internal ShowExerciseContent(string downloadLink,string exerciseName,string videoCopyright,string uid, DisplayPatientExercisePlanViewModel viewModel,int whichExercise,int min,int max)
        {
            this.downloadLink = downloadLink;
            name = exerciseName;
            copyright = videoCopyright;
            this.uid = uid;
            this.whichExercise = whichExercise;
            model = viewModel;
            this.min = min;
            this.max = max;
            InitializeComponent();
        }
        /// <summary>
        /// This method adds the exercise name to the label and copyright to the video
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            exer.Text = "Reps to complete " + min + " to " + max;
            exerciseLabel.Text = name;
            copyRightLabel.Text = copyright;
        }
        /// <summary>
        /// This button method uses the download link to play the video to the mobile screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void showVideo(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Play(downloadLink);
        }
        /// <summary>
        /// This button method can pause resume the video when the button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void pauseVideo(object sender, EventArgs e)
        {
           await CrossMediaManager.Current.PlayPause();
        }
        /// <summary>
        /// This button method stops the video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void endVideo(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();
        }
        /// <summary>
        /// This button sets the exerciseComplete variable to true stating they have finsihed their exercise for the day
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void exercise(object sender, EventArgs e)
        {
            exerciseComplete = true;
            exer.IsEnabled = false;
        }
        void exerciseValue(object sender, TextChangedEventArgs e)
        {
            int num = 0;
            int.TryParse(e.NewTextValue, out num);
            if (num >= min && num <= max)//keep at min or make zero
            {
                exerciseComplete = true;
                numExercises.IsEnabled = false;
            }
            else
            {
                numExercises.Text = "";
            }
        }
        /// <summary>
        /// This button confirms that the patient has finshed their exercise by seting a property value in the 
        /// DisplayPatientExercisePlanViewModel view model and returning to the previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void completed(object sender, EventArgs e)
        {
            if (exerciseComplete)
            {
                model.ExerciseCompletedNumber = whichExercise;
                await Application.Current.MainPage.Navigation.PopModalAsync(true);
            }
            else
            {
                await DisplayAlert("Message", "Please press button accross from Reps to complete than select complete", "OK");
            }
            
        }
        /// <summary>
        /// This button returns to user to the previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void returned(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }
}