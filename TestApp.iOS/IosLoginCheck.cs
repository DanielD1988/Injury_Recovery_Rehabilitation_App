﻿using Firebase.Auth;
using System;
using System.Threading.Tasks;
using TestApp.iOS;
using TestApp.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosLoginCheck))]
namespace TestApp.iOS
{
    /// <summary>
    /// This class inherts LoginWithEmailPassword and SignInWithEmailAndPasswordAsync methods from IFirebaseAuthenticator
    /// which is used to check if a user is authorised to login
    /// </summary>
    class IosLoginCheck : IFirebaseAuthenticator
    {
        /// <summary>
        /// This method takes the users login and password uses FirebaseAuth to check if the user is verified
        /// and returns a token if they were logged in successfully
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// </summary>
        //https://medium.com/firebase-developers/firebase-auth-on-xamarin-forms-171432aa3d76
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch (Exception e)
            {
                var error = e.StackTrace;
                Console.WriteLine(error);
                return "";
            }

        }/// <summary>
         /// This method allows a user to register with the application through firebaseAuth
         /// </summary>
         /// <param name="email"></param>
         /// <param name="password"></param>
         /// <returns></returns>
        //https://www.py4u.net/discuss/1541967
        public async Task<string> SignupWithEmailPassword(string email, string password)
        {
            var authDataResult = await Auth.DefaultInstance.CreateUserAsync( email, password);

            return await authDataResult.User.GetIdTokenAsync();
        }
    }
}