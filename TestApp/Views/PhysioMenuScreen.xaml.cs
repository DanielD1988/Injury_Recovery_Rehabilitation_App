using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        string physioId = "";
        List<PatientDetails> details =  new List<PatientDetails>();
        PlanProgressViewModel model = new PlanProgressViewModel();
        SecurityViewModel security = new SecurityViewModel();
        public PhysioMenuScreen(string uid)
        {
            InitializeComponent();
            physioId = uid;
        }
        protected async override void OnAppearing()
        {
            if(details.Count == 0)
            {
                var namedetails = await model.getPatientNameAndPatientUserId(physioId);
                if(namedetails.Count != 0)
                {
                    var encyptKeys = await security.getPatientEncryptionKeys(false);
                    details = await security.decyptPatientNames(namedetails, encyptKeys);
                }
            }
            base.OnAppearing();
            imgageDisplay.Source = "danLogo.png";
        }
        async void goToSelectExercisePlan(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new DisplayExercises(physioId,details));
        }
        async void viewPatientsProgress(object sender, EventArgs args)
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