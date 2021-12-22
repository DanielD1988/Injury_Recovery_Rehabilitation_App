using System;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPatient : ContentPage
    {
        string gender = "";
        string startDate = "";
        string patientName = "";
        int patientAge = 0;
        string patientEmail = "";
        int severityNumber = 0;
        string injuryType = "";
        string newInjuryType = "";
        string injuryOccurred = "";
        string newinjuryOccurred = "";
        string endDate = "";
        bool infoCorrect = true;
        string errorMessage = "";
        RPatientViewModel patientVm;
        public RegisterPatient()
        {
            InitializeComponent();
            patientVm = new RPatientViewModel();
        }
        void getGender(object sender, CheckedChangedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            gender = button.Content.ToString();
        }
        async void PatientRegister(object sender, EventArgs e)
        {
            if(gender == "")
            {
                errorMessage += "Please select a gender\n";
                infoCorrect = false;
            }
            patientName = name.Text;
            if(patientName == null)
            {
                errorMessage += "Please enter patient name\n";
                infoCorrect = false;
            }

            int.TryParse(age.Text,out patientAge);
            if(patientAge == 0)
            {
                errorMessage += "Please enter patient age\n";
                infoCorrect = false;
            }

            patientEmail = email.Text;
            if(patientEmail == null)
            {
                errorMessage += "Please enter a valid patient email\n";
                infoCorrect = false;
            }
            else
            {
                if(patientEmail.Contains("@") && patientEmail.Contains(".")){

                }
                else
                {
                    infoCorrect = false;
                }
            }

            try
            {
                int.TryParse(SeverityPicker.SelectedItem.ToString(), out severityNumber);
            }
            catch(NullReferenceException error)
            {
                errorMessage += "Please pick a severity number\n";
                infoCorrect = false;
            }
            newInjuryType = injury.Text;
            if (newInjuryType == null)
            {
                try
                {
                    injuryType = Injurypicker.SelectedItem.ToString();
                }
                catch (NullReferenceException error)
                {
                    errorMessage += "Please pick an injury Type or enter a new injury type info\n";
                    infoCorrect = false;
                }
            }

            newinjuryOccurred = occured.Text;
            if (newinjuryOccurred == null)
            {
                try
                {
                    injuryOccurred = Occurredpicker.SelectedItem.ToString();
                }
                catch (NullReferenceException error)
                {
                    errorMessage += "Please pick how Injury occurred or enter additional injury info\n";
                    infoCorrect = false;
                }
            }
            DateTime today = DateTime.Today;
            startDate = startDatePicker.Date.ToString();
            startDate = patientVm.removeTimeFromDate(startDate);
            endDate = endDatePicker.Date.ToString();
            endDate = patientVm.removeTimeFromDate(endDate);

            DateTime sDate = Convert.ToDateTime(startDate);
            DateTime nDate = Convert.ToDateTime(endDate);
            int result1 = DateTime.Compare(sDate, today);
            int result2 = DateTime.Compare(nDate, today);
            if (result1 < 0 || result2 < 0)
            {
                errorMessage += "Please pick a date from today onwards\n";
                infoCorrect = false;
            }
            if(infoCorrect == false)
            {
                await DisplayAlert("Error", errorMessage, "OK");
            }
        }
    }
}