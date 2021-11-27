using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This class is used to send login info to a patient
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sendEmailToPatient : ContentPage
    {
        public sendEmailToPatient(String exerciseKey)
        {
            InitializeComponent();

        }
        /// <summary>
        /// This button when pressed takes the entered email of the patient and adds it to the 
        /// list of recipients and sends it to the sendEmail method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void btnSend_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                List<string> recipients = new List<string>();
                recipients.Add(txtTo.Text);
               
                await SendEmail(recipients);
                await Navigation.PushModalAsync(new DisplayExercises());
            }
            catch (Exception ex)
            {
                await DisplayAlert ("Faild", ex.Message, "OK");
            }
        }
        /// <summary>
        /// This method uses xamarin essentials to open an email application on the users phone and populates the body
        /// ,the patients email and the subject of the email it returns a bool for testing purposes.
        /// </summary>
        /// <param name="recipients"></param>
        /// <returns></returns>
        //https://docs.microsoft.com/en-us/xamarin/essentials/email?tabs=ios
        public async Task<bool> SendEmail(List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "Login Details",
                    Body = "Please find attached login details",
                    To = recipients,
                };
                await Email.ComposeAsync(message);
                return true;
            }
            catch (FeatureNotSupportedException ns)
            {
               var value = ns.StackTrace;
               Console.WriteLine(value);
               await DisplayAlert("Error", "email Not supported on this device", "ok");
               return false;
            }
            catch (Exception ex)
            {
               var value = ex.StackTrace;
               Console.WriteLine(value);
               await DisplayAlert("Error", "Error when opening email application", "ok");
               return false;
            }
        }///////////////////////////////////////////////////////////////////////////////////
    }
}