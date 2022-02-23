using System;
using System.Collections.Generic;
using TestApp.models;
using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddToExistingPatient : ContentPage
    {
        string physioUid = "";
        List<PatientList> details;
        CurrentPatientViewModel patientVm = new CurrentPatientViewModel();
        SecurityViewModel security = new SecurityViewModel();
        string patientName = "";
        string patientUid = "";
        string encryptionKey = "";
        int min1 = 0;
        int min2 = 0;
        int min3 = 0;
        int max1 = 0;
        int max2 = 0;
        int max3 = 0;
        ExercisePlan exercisePlan;
        public AddToExistingPatient(ExercisePlan exercisePlan, string physioUid, int min1, int min2, int min3, int max1, int max2, int max3, List<PatientList> details)
        {
            InitializeComponent();
            this.physioUid = physioUid;
            this.min1 = min1;
            this.min2 = min2;
            this.min3 = min3;
            this.max1 = max1;
            this.max2 = max2;
            this.max3 = max3;
            this.details = details;
            this.exercisePlan = exercisePlan;
        }
        /// <summary>
        /// As the screen loads a list of patient names assigned to the physio are added to the picker
        /// </summary>
        protected async override void OnAppearing()
        {   
            base.OnAppearing();
            List<string> names = new List<string>();
            foreach (PatientList detail in details)
            {
                names.Add(detail.PatientName);
            }
            namesPicker.ItemsSource = names;
        }
        /// <summary>
        /// This button uses the security view model to decrypt the patient data before passing it to the next screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void sendExercisePlan(object sender, EventArgs e)
        {
            if (namesPicker.SelectedIndex != -1)
            {
                patientName = namesPicker.SelectedItem.ToString();
                foreach (PatientList detail in details)
                {
                    if (patientName == detail.PatientName)
                    {
                        patientUid = detail.PatientUid;
                    }
                }
                Patient patient = await patientVm.getpatientDetails(patientUid, false);
                encryptionKey = await security.getEncryptionKey(false, patientUid);
                patient.PatientName = security.decryptData(patient.PatientName, encryptionKey);
                patient.Email = security.decryptData(patient.Email, encryptionKey);
                await Navigation.PushModalAsync(new DisplayPatientDetails(patient,exercisePlan,patientUid,min1,min2,min3,max1,max2,max3,physioUid, encryptionKey));
            }
            else
            {
                await DisplayAlert("Error", "Please select one of the patient names", "OK");
            }
        }
    }
}