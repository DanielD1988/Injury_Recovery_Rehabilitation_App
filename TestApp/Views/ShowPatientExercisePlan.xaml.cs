using MediaManager;
using System;
using System.Collections.Generic;
using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowPatientExercisePlan : ContentPage
    {
        DisplayPatientExercisesViewModel display;
        string downLoadLink1 = "";
        string downLoadLink2 = "";
        string downLoadLink3 = "";
        string patientUid = "";
        
        public ShowPatientExercisePlan(string uid)
        {
            display = new DisplayPatientExercisesViewModel();
            patientUid = uid;
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<Exercise> patientExerciselist = await display.getPatientsExercisePlan(patientUid, false);
            exerciseLabel1.Text = patientExerciselist[0].ExerciseName;
            copyRightLabel1.Text = "Copyright " +  patientExerciselist[0].exerciseVideoCopyright;
            downLoadLink1 = await display.getExerciseVideoLink(patientExerciselist[0].VideoLink += ".mp4", false);
            exerciseLabel2.Text = patientExerciselist[1].ExerciseName;
            copyRightLabel2.Text = "Copyright " + patientExerciselist[1].exerciseVideoCopyright;
            downLoadLink2 = await display.getExerciseVideoLink(patientExerciselist[1].VideoLink += ".mp4", false);
            exerciseLabel3.Text = patientExerciselist[2].ExerciseName;
            copyRightLabel3.Text = "Copyright " +  patientExerciselist[2].exerciseVideoCopyright;
            downLoadLink3 = await display.getExerciseVideoLink(patientExerciselist[2].VideoLink += ".mp4", false);
        }
        private async void showVideo1(object sender, EventArgs e)
        {
            if (PlayVideo1.Text == "Play")
            {
                await CrossMediaManager.Current.Play(downLoadLink1);

                PlayVideo1.Text = "Stop";
            }
            else if (PlayVideo1.Text == "Stop")
            {
                await CrossMediaManager.Current.Stop();

                PlayVideo1.Text = "Play";
            }
        }
        private async void showVideo2(object sender, EventArgs e)
        {
            if (PlayVideo2.Text == "Play")
            {
                 await CrossMediaManager.Current.Play(downLoadLink2);

                PlayVideo2.Text = "Stop";
            }
            else if (PlayVideo2.Text == "Stop")
            {
                await CrossMediaManager.Current.Stop();

                PlayVideo2.Text = "Play";
            }
        }
        private async void showVideo3(object sender, EventArgs e)
        {
            if (PlayVideo3.Text == "Play")
            {
                await CrossMediaManager.Current.Play(downLoadLink3);

                PlayVideo3.Text = "Stop";
            }
            else if (PlayVideo3.Text == "Stop")
            {
                await CrossMediaManager.Current.Stop();

                PlayVideo3.Text = "Play";
            }
        }
    }
}