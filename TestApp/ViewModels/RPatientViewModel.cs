using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using TestApp.services;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class passes patient information to the database 
    /// Generates a unique password for a patients login
    /// Generates an email so the login information can be sent to the patient
    /// </summary>
    class RPatientViewModel
    {
        string password = "";
        //IFirebaseAuthenticator auth = DependencyService.Get<IFirebaseAuthenticator>();
        IFirebaseAuthenticator auth;
        FirebaseMethods fireBase;
        List<string> patientEmailList;
        public RPatientViewModel(IFirebaseAuthenticator auth)
        {
            this.auth = auth;
            //auth = DependencyService.Get<IFirebaseAuthenticator>();
            fireBase = FirebaseMethods.GetInstance();
            patientEmailList = new List<string>();
        }
        /// <summary>
        /// This method removes the time from the data time object
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string removeTimeFromDate(string dateTime)
        {
            int spaceFound = dateTime.IndexOf(" ");
            string date = dateTime.Substring(0, spaceFound);
            return date;
        }
        /// <summary>
        /// This method takes data from Register patient view and
        /// sends a generated password and the patients email to make an account for that patient
        /// then uses the user id to fill in the rest of the patients details into the patients database
        /// the user iid is then added to the physios patients list
        /// this method calls the method to send login details to the patient by email 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="email"></param>
        /// <param name="injuryType"></param>
        /// <param name="injuryOccurred"></param>
        /// <param name="age"></param>
        /// <param name="injurySeverity"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="exerPlan"></param> 
        /// <param name="physioUid"></param>
        public async Task<bool> setupUserAccount(string name, string gender,string email ,string injuryType, string injuryOccurred, int age, int injurySeverity, DateTime start, DateTime end, string exerPlan,string physioUid,string newInjuryType,string newinjuryOccurred)
        {
            try
            {
                if (newInjuryType != null)
                {
                    injuryType = newInjuryType;
                }
                if (newinjuryOccurred != null)
                {
                    injuryOccurred = newinjuryOccurred;
                }
                patientEmailList.Add(email);
                password = generatePatientPassword();
                password = password.Replace("-", "");
                password += "p";
                string patientUid = await auth.SignupWithEmailPassword(email, password);
                await fireBase.AddPatient(patientUid, name, gender, injuryType, injuryOccurred, age, injurySeverity, start, end, exerPlan, email,false);
                await fireBase.AddPatientUIDToPatientList(physioUid, patientUid,false);
                await SendPatientEmail(patientEmailList, password);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            
        }
        /// <summary>
        /// This method uses the RNGCryptoServiceProvider class generates random numbers
        /// This GetBytes method takes in a byte array and fills byte array with random values
        /// and returns a string from the byte array
        /// </summary>
        /// <returns></returns>
        public string generatePatientPassword()
        {
            RNGCryptoServiceProvider encrypt = new RNGCryptoServiceProvider();
            byte[] random = new byte[15];
            encrypt.GetBytes(random);
            return BitConverter.ToString(random);
        }
        /// <summary>
        /// This method uses xamarin essentials to open an email application on the users phone and populates the body
        /// the patients email and the subject of the email it returns a bool for testing purposes.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //https://docs.microsoft.com/en-us/xamarin/essentials/email?tabs=ios
        public async Task<bool> SendPatientEmail(List<string> recipients, string password)
        {
            try
            {
                EmailMessage message = new EmailMessage
                {
                    Subject = "Injury Recovery Login Details",
                    Body = "Please find attached login details,\n Use this email and here is the password " + password,
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
