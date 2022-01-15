using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.services;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class has methods to ecrypt passwords for login
    /// </summary>
    class PasswordSecuirty
    {
        private FirebaseMethods fire;
        public PasswordSecuirty()
        {
            fire = FirebaseMethods.GetInstance();
        }
        /// <summary>
        /// This method uses the RNGCryptoServiceProvider class generates random numbers
        /// This GetBytes method takes in a byte array and fills byte array with random values
        /// and returns a string from the byte array
        /// </summary>
        /// <param name="numberOfBytes"></param>
        /// <returns></returns>
        public string generateSaltOrPasswordOrUid(int numberOfBytes)
        {
            RNGCryptoServiceProvider encrypt = new RNGCryptoServiceProvider();
            byte[] random = new byte[numberOfBytes];
            encrypt.GetBytes(random);
            return BitConverter.ToString(random).Replace("-", "");
        }
        /// <summary>
        /// This method generates an md5 encrypted password from the salt and password
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string md5HashAndSaltThePassword(string salt, string password)//https://riptutorial.com/csharp/example/9341/md5
        {
            var sourceBytes = Encoding.UTF8.GetBytes(salt + password);
            var md5Hash = MD5.Create();
            var hashedString = md5Hash.ComputeHash(sourceBytes);
            string newPassword = BitConverter.ToString(hashedString).Replace("-", "");
            return newPassword;
        }
        /// <summary>
        /// This method checks if the user is verified to access the app
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> checkIfLoginIsVerified(string email,string password)
        {
            IosCredentials securityDetails = await fire.getIosPatientPasswordDetails(false,email);
            string hashedAndSaltedPassword = md5HashAndSaltThePassword(securityDetails.salt, password);
            
            if (securityDetails.saltHashed.Contains("p"))
            {
                hashedAndSaltedPassword = hashedAndSaltedPassword += "p";
                
            }
            if (hashedAndSaltedPassword.Equals(securityDetails.saltHashed))
            {
                return securityDetails.userId;
            }
            else
            {
                return "";
            }
        }
    }
}
