using System.Collections.Generic;
using TestApp.ViewModels;
using Xunit;

namespace UnitTestProject
{
    public class RPatientViewModelTest
    {
        RPatientViewModel patientVm = new RPatientViewModel(null);
        /// <summary>
        /// This test checks if the time is striped from the date time object
        /// </summary>
        [Fact]
        public void StripeTimeFromDateTest()
        {
            string dateTime = "29/12/2021 00:00:00";
            string result = patientVm.removeTimeFromDate(dateTime);
            int expected = 10;
            Assert.Equal(expected,result.Length);
        }
        /// <summary>
        /// This test checks if a generated password is of a certain length
        [Fact]
        public void GeneratePatientPasswordTest()
        {
            string password = patientVm.generatePatientPassword();
            int expected = 44;
            Assert.Equal(expected, password.Length);
        }
        /// <summary>
        /// This method checks if the SendPatientEmail is called can not open an xamarin essentails email from a test 
        /// so test does fail
        /// </summary>
        [Fact]
        public void SendPatientEmailTest()
        {
            List<string> emailList = new List<string>();
            emailList.Add("john@gmail.com");
            bool result = patientVm.SendPatientEmail(emailList, "password").Result;
            bool expected = false;
            Assert.Equal(expected, result);
        }
    }
}
