using System;
using TestApp.models;
using TestApp.ViewModels;
using TestApp.views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This class shows a register patient form
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPatient : ContentPage
    {
        ExercisePlan exerPlan = null;
        string gender = "";
        string startDate = "";
        string patientName = "";
        int patientAge = 0;
        string patientEmail = "";
        int severityNumber = 0;
        string injuryType = "";
        string newInjuryType = null;
        string injuryOccurred = "";
        string newinjuryOccurred = null;
        string endDate = "";
        bool infoCorrect = true;
        string errorMessage = "";
        string physioUid = "";
 
        RadioButton button;
        RegisterPatientViewModel patientVm;
        IFirebaseAuthenticator auth = DependencyService.Get<IFirebaseAuthenticator>();
        /// <summary>
        /// This constructor takes in the selected exercise plan and physiotherapist Id 
        /// </summary>
        /// <param name="exercisePlan"></param>
        /// <param name="physioUid"></param>
        public RegisterPatient(ExercisePlan exercisePlan, string physioUid)
        {
            InitializeComponent();
            exerPlan = exercisePlan;
            this.physioUid = physioUid;
            patientVm = new RegisterPatientViewModel(auth);
            injury.IsEnabled = false;
            injury.IsVisible = false;
            InjuryLabel.IsVisible = false;
            occured.IsEnabled = false;
            occured.IsVisible = false;
            OccuredLabel.IsVisible = false;
        }
        /// <summary>
        /// Radio button for gender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void getGender(object sender, CheckedChangedEventArgs e)
        {
            button = sender as RadioButton;
            gender = button.Content.ToString();
        }
        /// <summary>
        /// This picker event hides the Optional Other Injury Type entry if something is picked on the picker 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InjuryPicked(object sender, EventArgs e)
        {
            injuryType = Injurypicker.SelectedItem.ToString();
            if (injuryType == "Enter Your Own")
            {
                InjuryLabel.IsVisible = true;
                injury.IsVisible = true;
                injury.IsEnabled = true;
            }
            else
            {
                injury.Text = null;
                injury.IsEnabled = false;
                injury.IsVisible = false;
                InjuryLabel.IsVisible = false;
            }
        }
        /// <summary>
        /// This picker event hides the Optional Additional Injury entry if something is picked on the picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InjuryOccurred(object sender, EventArgs e)
        {
            injuryOccurred = Occurredpicker.SelectedItem.ToString();
            if (injuryOccurred == "Enter Your Own")
            {
                OccuredLabel.IsVisible = true;
                occured.IsVisible = true;
                occured.IsEnabled = true;
            }
            else
            {
                occured.Text = null;
                occured.IsEnabled = false;
                occured.IsVisible = false;
                OccuredLabel.IsVisible = false;
            }
        }
        /// <summary>
        /// This button checks the validation of the entered form data before sending it to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void PatientRegister(object sender, EventArgs e)
        {
            if (gender == "")
            {
                errorMessage += "Please select a gender\n";
                infoCorrect = false;
            }
            patientName = name.Text;
            if (patientName == null)
            {
                errorMessage += "Please enter patient name\n";
                infoCorrect = false;
            }

            int.TryParse(age.Text, out patientAge);
            if (patientAge == 0 || patientAge < 0)
            {
                errorMessage += "Please enter patient age\n";
                infoCorrect = false;
            }

            patientEmail = email.Text;
            if (patientEmail == null)
            {
                errorMessage += "Please enter a valid patient email\n";
                infoCorrect = false;
            }
            else
            {
                if (patientEmail.Contains("@") && patientEmail.Contains("."))
                {

                }
                else
                {
                    errorMessage += "Please enter a valid email\n";
                    infoCorrect = false;
                }
            }

            try
            {
                int.TryParse(SeverityPicker.SelectedItem.ToString(), out severityNumber);
            }
            catch (NullReferenceException error)
            {
                Console.WriteLine(error.StackTrace);
                errorMessage += "Please pick a severity number\n";
                infoCorrect = false;
            }
            
            newInjuryType = injury.Text;
            if (newInjuryType == null)
            {
                try
                {
                    injuryType = Injurypicker.SelectedItem.ToString();
                    if (injuryType == "Enter Your Own")
                    {
                        errorMessage += "Please pick an injury Type or enter a new injury type info\n";
                        infoCorrect = false;
                    }
                }
                catch (NullReferenceException error)
                {
                    Console.WriteLine(error.StackTrace);
                    errorMessage += "Please pick an injury Type or enter your own\n";
                    infoCorrect = false;
                }
            }

            newinjuryOccurred = occured.Text;
            if (newinjuryOccurred == null)
            {
                try
                {
                    injuryOccurred = Occurredpicker.SelectedItem.ToString();
                    if (injuryOccurred == "Enter Your Own")
                    {
                        errorMessage += "Please pick how Injury occurred or enter your own\n";
                        infoCorrect = false;
                    }

                }
                catch (NullReferenceException error)
                {
                    Console.WriteLine(error.StackTrace);
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

            if (infoCorrect == false)
            {
                await DisplayAlert("Error", errorMessage, "OK");
                infoCorrect = true;
            }
            else
            {
                await patientVm.setUpPatientAccount(patientName, gender, patientEmail, injuryType, injuryOccurred, patientAge, severityNumber, sDate, nDate, exerPlan, physioUid, newInjuryType, newinjuryOccurred);
                await Navigation.PushModalAsync(new DisplayExercises("physioUid"));
            }
        }
    }
}