using System;
using System.Collections.Generic;
using TestApp.ViewModels;
using Xunit;

namespace UnitTestProject
{
    public class EmailTest
    {
        GenerateEmail email = new GenerateEmail();
        /// <summary>
        /// This test will fail as it needs the emulator open to open the email application
        /// </summary>
        [Fact]
        public async void checkIfEmailOpen()
        {
            List<string> emailAddress = new List<string>();
            emailAddress.Add("");
            bool value = await email.SendEmail(emailAddress);
            Assert.False(value);
        }
    }
}
