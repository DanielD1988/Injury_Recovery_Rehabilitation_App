using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhysioMenuScreen : ContentPage
    {
        string physioId = "";
        public PhysioMenuScreen(string uid)
        {
            InitializeComponent();
            physioId = uid;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            imgageDisplay.Source = "danLogo.png";
        }
        async void goToSelectExercisePlan(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new DisplayExercises(physioId));
        }
        async void viewPatientsProgress(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new SelectPatientToView(physioId));
        }
    }
}