using System;
using System.Collections.Generic;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientMenuScreen : ContentPage
    {
        private DisplayPatientExercisePlanViewModel model = new DisplayPatientExercisePlanViewModel();
        private PlanProgressViewModel plan = new PlanProgressViewModel();
        private string patientId = "";
        private DateTime today;
        private Dictionary<string, bool> progressPlan;
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
        private async void goToExercisePlan(object sender, EventArgs args)
        {
            today = DateTime.Today;
            string date = today.ToString("dd/MM/yyyy");
            date = date.Replace("/", "-");
            bool isCompleteForToday = await model.CheckIfExercisePlanCompleteForToday(date, patientId,false);
            if (isCompleteForToday)
            {
                await DisplayAlert("Error", "You have already completed your exercise for today", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new ShowPatientExercisePlan(patientId));
            }
        }
        private async void goToPatientProgress(object sender, EventArgs args)
        {
            progressPlan = await plan.getPatientProgress(patientId);
            await Navigation.PushModalAsync(new DisplayProgress(patientId,progressPlan));
        }
    }
}