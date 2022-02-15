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
        public PhysioMenuScreen(string uid)
        {
            InitializeComponent();
            physioId = uid;
        }
        protected async override void OnAppearing()
        {
            if(details.Count == 0)
            {
                details = await model.getPatientNameAndPatientUserId(physioId);
            }
            base.OnAppearing();
            imgageDisplay.Source = "danLogo.png";
        }
        async void goToSelectExercisePlan(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new DisplayExercises(physioId));
        }
        async void viewPatientsProgress(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new SelectPatientToView(details));
        }
    }
}