using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPatientToView : ContentPage
    {
        PlanProgressViewModel model = new PlanProgressViewModel();
        List<PatientDetails> details;
        string physioUid = "";
        string patientUid = "";
        string patientName = "";
        public SelectPatientToView(string physioUid)
        {
            InitializeComponent();
            this.physioUid = physioUid;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            details = await model.getPatientNameAndPatientUserId(physioUid);
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
                await Navigation.PushModalAsync(new DisplayProgress(patientUid));
            }
            else
            {
                await DisplayAlert("Error", "Please select one of the patient names", "OK");
            }
        }
    }
}