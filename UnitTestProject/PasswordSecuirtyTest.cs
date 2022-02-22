using System;
using System.Collections.Generic;
using System.Text;
using TestApp.ViewModels;
using Xunit;

namespace UnitTestProject
{
    public class PasswordSecuirtyTest
    {
        SecurityViewModel secuirty = new SecurityViewModel();
        /// <summary>
        /// checks if the method generates a random string of a certain length
        /// </summary>
        [Fact]
        public void generateSaltOrPasswordOrUidTest()
        {
            string result = secuirty.generateSaltOrPasswordOrUid(20);

        }
        /// <summary>
        /// checks if the method generates a random string of a certain length
        /// </summary>
        [Fact]
        public void md5HashAndSaltThePasswordTest()
        {
            string result = secuirty.md5HashAndSaltThePassword("5RGHVBDUYIM","GOODpASS45678");

        }
    }
}
