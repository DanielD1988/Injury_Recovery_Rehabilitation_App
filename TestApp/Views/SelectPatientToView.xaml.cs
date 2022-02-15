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
        List<PatientDetails> details;
        Dictionary<string, bool> progressPlan;
        PlanProgressViewModel plan = new PlanProgressViewModel();
        string patientUid = "";
        string patientName = "";
        public SelectPatientToView(List<PatientDetails> details)
        {
            InitializeComponent();
            this.details = details;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<string> names = new List<string>();
            foreach(PatientDetails detail in details)
            {
                names.Add(detail.Name);
            }
            namesPicker.ItemsSource = names;
        }
        async void viewProgress(object sender, EventArgs e)
        {
            if(namesPicker.SelectedIndex != -1)
            {
                patientName = namesPicker.SelectedItem.ToString();
                foreach (PatientDetails detail in details)
                {
                    if(patientName == detail.Name)
                    {
                        patientUid = detail.Uid;
                    }
                }
                progressPlan = await plan.getPatientProgress(patientUid);
                await Navigation.PushModalAsync(new DisplayProgress(patientUid, progressPlan,2));
            }
            else
            {
                await DisplayAlert("Error", "Please select one of the patient names", "OK");
            }
        }
    }
}