using Android.Gms.Extensions;
using Firebase.Auth;
using System.Threading.Tasks;
using TestApp.Droid;
using TestApp.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidoginCheck))]
namespace TestApp.Droid
{
    /// <summary>
    /// This class inherts LoginWithEmailPassword method from IFirebaseAuthenticator
    /// which is used to check if a user is authorised to login
    /// </summary>
    public class AndroidoginCheck : IFirebaseAuthenticator
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
                var user = await FirebaseAuth.Instance.
                            SignInWithEmailAndPasswordAsync(email, password);
                //https://github.com/xamarin/GooglePlayServicesComponents/issues/391
                var token = await (FirebaseAuth.Instance.CurrentUser.GetIdToken(false).AsAsync<GetTokenResult>());
                return token.Token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }
        }////////////////////////////////////////////////////////////////////
    }
}