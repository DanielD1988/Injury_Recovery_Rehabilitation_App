using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class will generate an email with a login for a patient
    /// </summary>
    class GenerateEmail
    {

        public GenerateEmail()
        {

        }
        /// <summary>
        /// This method uses xamarin essentials to open an email application on the users phone and populates the body
        /// the patients email and the subject of the email it returns a bool for testing purposes.
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
                return false;
            }
            catch (Exception ex)
            {
                var value = ex.StackTrace;
                Console.WriteLine(value);
                return false;
            }
        }///////////////////////////////////////////////////////////////////////////////////
    }
}
