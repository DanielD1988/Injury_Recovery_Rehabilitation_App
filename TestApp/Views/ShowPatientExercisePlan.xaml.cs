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
        bool exercise1Complete = false;
        bool exercise2Complete = false;
        bool exercise3Complete = false;
        string videoFile = "";
        string patientUid = "";
        string exerciseName = "";
        string videoCopyRight = "";
        List<Exercise> patientExerciselist = new List<Exercise>();
        
        public ShowPatientExercisePlan(string uid)
        {
            display = new DisplayPatientExercisesViewModel();
            patientUid = uid;
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(patientExerciselist.Count == 0)
            {
                patientExerciselist = await display.getPatientsExercisePlan(patientUid, false);
            }
            //patientExerciselist = await display.getPatientsExercisePlan(patientUid, false);
            exerciseLabel1.Text = patientExerciselist[0].ExerciseName;
            exerciseLabel2.Text = patientExerciselist[1].ExerciseName;
            exerciseLabel3.Text = patientExerciselist[2].ExerciseName;
        }
        async void exercise1(object sender, EventArgs e)
        {
            videoFile = "";
            videoFile = patientExerciselist[0].VideoLink += ".mp4";
            downLoadLink1 = await display.getExerciseVideoLink(videoFile, false);
            exerciseName = patientExerciselist[0].ExerciseName;
            videoCopyRight = "Video Copyright " + patientExerciselist[0].exerciseVideoCopyright;
            await Navigation.PushModalAsync(new ShowExerciseContent(downLoadLink1, exerciseName, videoCopyRight, patientUid));
            //progress.Progress += 0.33;
            //exer1.IsEnabled = false;
        }
        async void exercise2(object sender, EventArgs e)
        {
            videoFile = "";
            videoFile = patientExerciselist[1].VideoLink += ".mp4";
            downLoadLink2 = await display.getExerciseVideoLink(videoFile, false);
            exerciseName = patientExerciselist[1].ExerciseName;
            videoCopyRight = "Video Copyright " + patientExerciselist[1].exerciseVideoCopyright;
            await Navigation.PushModalAsync(new ShowExerciseContent(downLoadLink2, exerciseName, videoCopyRight, patientUid));
            //progress.Progress += 0.33;
            //exer2.IsEnabled = false;
        }
        async void exercise3(object sender, EventArgs e)
        {
            videoFile = "";
            videoFile = patientExerciselist[2].VideoLink += ".mp4";
            downLoadLink3 = await display.getExerciseVideoLink(videoFile, false);
            exerciseName = patientExerciselist[2].ExerciseName;
            videoCopyRight = "Video Copyright " + patientExerciselist[2].exerciseVideoCopyright;
            await Navigation.PushModalAsync(new ShowExerciseContent(downLoadLink3, exerciseName, videoCopyRight, patientUid));
            //progress.Progress += 0.33;
            //exer3.IsEnabled = false;
        }
        
        private void saveResult(object sender, EventArgs e)
        {
            if(exercise1Complete == true && exercise2Complete == true && exercise3Complete == true)
            {
                DateTime dateTime = DateTime.Today;
                var currentDateTime = dateTime;
                string date = currentDateTime.Date.ToString();
                //send uid, date and true value
            }    
        }
    }
}