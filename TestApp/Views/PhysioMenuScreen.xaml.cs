using System;
using System.Collections.Generic;
using TestApp.Models;
using TestApp.ViewModels;
using TestApp.views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhysioMenuScreen : ContentPage
    {
        private string physioId = "";
        private List<PatientList> details =  new List<PatientList>();
        private PlanProgressViewModel model = new PlanProgressViewModel();
        private SecurityViewModel security = new SecurityViewModel();
        
        public PhysioMenuScreen(string uid)
        {
            InitializeComponent();
            physioId = uid;
        }
        /// <summary>
        /// As the screen loads the encrypted patient names and patient user ids tied to the physio are retrieved
        /// The encryption keys that match the patient user id are used to decypt the patient names
        /// </summary>
        protected async override void OnAppearing()
        {
            if(details.Count == 0)
            {
                var namedetails = await model.getPatientNameAndPatientUserId(physioId,false);
                if(namedetails.Count != 0)
                {
                    var encyptKeys = await security.getPatientEncryptionKeys(false);
                    details = await security.decyptPatientNames(namedetails, encyptKeys);
                }
            }

            base.OnAppearing();
            imgageDisplay.Source = "danLogo.png";
        }
        /// <summary>
        /// This button goes to the Display exercises screen with the physio user id and the list of patient names and ids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void goToSelectExercisePlan(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new DisplayExercises(physioId,details));
        }
        /// <summary>
        /// This button goes to the select patient to view screen alonng with the list of patient names and ids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void viewPatientsProgress(object sender, EventArgs args)
        {
            if(details.Count != 0)
            {
                await Navigation.PushModalAsync(new SelectPatientToView(details));
            }
            else
            {
                await DisplayAlert("Error", "You are not assigned any patients", "OK");
            }
        }
    }
}