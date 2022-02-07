using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientMenuScreen : ContentPage
    {
        string patientId = "";
        public PatientMenuScreen(string uid)
        {
            InitializeComponent();
            patientId = uid;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            imgageDisplay.Source = "danLogo.png";
        }
        async void goToExercisePlan(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new ShowPatientExercisePlan(patientId));
        }
        void goToPatientProgress(object sender, EventArgs args)
        {

        }
    }
}