using System.Threading.Tasks;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This interface method is used to tell mainPage.xaml.cs which os is trying to login
    /// </summary>
    //https://medium.com/firebase-developers/firebase-auth-on-xamarin-forms-171432aa3d76
    public interface IFirebaseAuthenticator
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<string> SignupWithEmailPassword(string email, string password);
    }
    /////////////////////////////////////////////////////////////////////////
}
