using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    class SecurityViewModel
    {
        private FirebaseMethods fire;
        private static readonly byte[] salt = Encoding.ASCII.GetBytes("Xamarin.iOS Version: 7.0.6.168");
        public SecurityViewModel()
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
           
            if (hashedAndSaltedPassword.Equals(securityDetails.saltHashed))
            {
                return securityDetails.userId;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// This method returns the type of user from database function
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> checkUserType(string userId)
        {
            CheckUser userType = await fire.getTypeOfUser(userId, false);
            if(userType == null)
            {
                return "";
            }
            else
            {
                return userType.User;
            }
        }
        /// <summary>
        /// This method encrypts a plain text word into an encrypted word using a encryptionKey
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public string encryptData(string plainText,string encryptionKey)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes("pemgail9uzpgzl88");
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(encryptionKey, null);
            byte[] keyBytes = password.GetBytes(256 / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.PKCS7;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }
        /// <summary>
        /// This method decrypts a cypher string using the encryptionKey into plain text
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public string decryptData(string cipherText, string encryptionKey)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes("pemgail9uzpgzl88");
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(encryptionKey, null);
            byte[] keyBytes = password.GetBytes(256 / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Padding = PaddingMode.PKCS7;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            return plainText;
        }
       
        /// <summary>
        /// This method returns a list of patient uids and encryption keys
        /// </summary>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<List<Encryption>> getPatientEncryptionKeys(bool isMocked)
        {
            return await fire.getPatientEncryptionKeys(isMocked);
        }
        /// <summary>
        /// This method returns the encryption key tied to the patient user id
        /// </summary>
        /// <param name="isMocked"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<string> getEncryptionKey(bool isMocked,string patientId)
        {
            return await fire.getKeyToData(patientId, isMocked);
        }
        /// <summary>
        /// This method converts encrypted text to plain text
        /// </summary>
        /// <param name="details"></param>
        /// <param name="keys"></param>
        public async Task<List<PatientDetails>> decyptPatientNames(List<PatientDetails> details,List<Encryption> keys)
        {
            string encryptionKey = "";
            var keysDict = keys.ToDictionary(x => x.PatientUid, x => x.EncryptionKey);
            foreach (PatientDetails detail in details)
            {
                if (keysDict.TryGetValue(detail.Uid,out encryptionKey))
                {
                    string plainText = decryptData(detail.Name,encryptionKey);
                    detail.Name = plainText;
                }
            }
            return details;
        }
    }
}
