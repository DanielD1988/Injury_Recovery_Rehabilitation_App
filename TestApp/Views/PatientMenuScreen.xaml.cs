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
        DisplayPatientExercisePlanViewModel model = new DisplayPatientExercisePlanViewModel();
        PlanProgressViewModel plan = new PlanProgressViewModel();
        string patientId = "";
        DateTime today;
        Dictionary<string, bool> progressPlan;
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
            today = DateTime.Today;
            string date = today.ToString("dd/MM/yyyy");
            date = date.Replace("/", "-");
            bool isCompleteForToday = await model.checkIfExercisePlanCompleteForToday(date, patientId);
            if (isCompleteForToday)
            {
                await DisplayAlert("Error", "You have already completed your exercise for today", "OK");
            }
            else
            {
                await Navigation.PushModalAsync(new ShowPatientExercisePlan(patientId));
            }
        }
        async void goToPatientProgress(object sender, EventArgs args)
        {
            progressPlan = await plan.getPatientProgress(patientId);
            await Navigation.PushModalAsync(new DisplayProgress(patientId,progressPlan));
        }
    }
}