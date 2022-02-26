using System;
using System.Threading.Tasks;
using TestApp.services;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class takes in the physio form data
    /// </summary>
    class RegisterPhysioViewModel
    {
        IFirebaseAuthenticator auth;
        FirebaseMethods fireBase;
        SecurityViewModel security = new SecurityViewModel();
        string salt = "";
        string saltedPassword = "";
        string physioUid = "";
        public RegisterPhysioViewModel(IFirebaseAuthenticator auth)
        {
            this.auth = auth;
            fireBase = FirebaseMethods.GetInstance();
        }
        public RegisterPhysioViewModel()//used for testing IFirebaseAuthenticator can not be run in unit test
        {
            fireBase = FirebaseMethods.GetInstance();
        }
        /// <summary>
        /// This function creates a record in the database for the newly signed up physio
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="physioIdNumber"></param>
        /// <param name="membership"></param>
        /// <returns></returns>
        public async Task<bool> setUpPhysioAccount(string email,string name,string physioIdNumber,string membership,string password,bool isMocked)
        {
            try
            {
                if(isMocked)
                {
                    physioUid = "";
                }
                else
                {
                    physioUid = await auth.SignupWithEmailPassword(email, password);
                }
                if (physioUid == "true")//this checks if the iOS SignupWithEmailPassword method is called
                {
                    salt = security.generateSaltOrPasswordOrUid(32);
                    saltedPassword = security.md5HashAndSaltThePassword(salt, password);
                    physioUid = security.generateSaltOrPasswordOrUid(10);
                    await fireBase.iOSSignupWithEmailPassword(email, salt, saltedPassword, physioUid, isMocked);
                }
                await fireBase.AddPhysio(physioUid, name, physioIdNumber, email, membership, isMocked);
                await fireBase.addUserType(physioUid, "physio", isMocked);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}
