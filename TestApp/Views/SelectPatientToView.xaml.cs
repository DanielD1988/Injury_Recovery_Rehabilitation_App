using System;
using System.Collections.Generic;
using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPatientToView : ContentPage
    {
        List<PatientList> details;
        Dictionary<string, bool> progressPlan;
        PlanProgressViewModel plan = new PlanProgressViewModel();
        string patientUid = "";
        string patientName = "";
        public SelectPatientToView(List<PatientList> details)
        {
            InitializeComponent();
            this.details = details;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<string> names = new List<string>();
            foreach(PatientList detail in details)
            {
                names.Add(detail.PatientName);
            }
            namesPicker.ItemsSource = names;
        }
        async void viewProgress(object sender, EventArgs e)
        {
            if(namesPicker.SelectedIndex != -1)
            {
                patientName = namesPicker.SelectedItem.ToString();
                foreach (PatientList detail in details)
                {
                    if(patientName == detail.PatientName)
                    {
                        patientUid = detail.PatientUid;
                    }
                }
                progressPlan = await plan.getPatientProgress(patientUid);
                await Navigation.PushModalAsync(new DisplayProgress(patientUid, progressPlan));
            }
            else
            {
                await DisplayAlert("Error", "Please select one of the patient names", "OK");
            }
        }
    }
}