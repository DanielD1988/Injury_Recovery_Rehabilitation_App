using MediaManager;
using System;
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
        public ShowPatientExercisePlan()
        {
            display = new DisplayPatientExercisesViewModel();
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            downLoadLink1 =  await display.getExerciseVideoLink("testing.mp4", false);
            downLoadLink2 = await display.getExerciseVideoLink("testing2.mp4", false);
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
    }
}