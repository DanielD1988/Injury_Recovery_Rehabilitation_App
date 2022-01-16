using System;
using System.Linq;
using TestApp.ViewModels;
using TestApp.views;
using TestApp.Views;
using Xamarin.Forms;
namespace TestApp
{
    /// <summary>
    /// This class is the first screen of the application allows a user to enter login details
    /// or go to the physio register page
    /// </summary>
    public partial class MainPage : ContentPage
    {
        IFirebaseAuthenticator auth;
        PasswordSecuirty secuirty;
        string [] SpecialCharacters = new[] { "~", "`", "!", "#", "$", "%", "^", "&", "*", "(", ")", "+", "=", "\"", "<", ">", "\'", "[", "]", "{", "}", "+","," };
        string newPass = "";
        string newEmail = "";
        bool emailCorrect = true;
        /// <summary>
        /// This is the login page for both apps
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            //Navigation.PushModalAsync(new DisplayExercises("QzkZZv9OxkNrxDTeex9lKEKUZ0C2"));
            //Navigation.PushModalAsync(new ShowPatientExercisePlan("Fhr3wnQjYTg2bJKXdsa99mjf0HR2"));
            //https://github.com/xamarin/GooglePlayServicesComponents/issues/391
            secuirty = new PasswordSecuirty();
            
            auth = DependencyService.Get<IFirebaseAuthenticator>();
            /////////////////////////////////////////////////////////////////////
        }
        void passwordtextChanged(object sender, TextChangedEventArgs e)
        {

            if(SpecialCharacters.Any(e.NewTextValue.Contains))
            {
                newPass = pass.Text;
                newPass = newPass.Remove(newPass.Length - 1);
                pass.Text = newPass;
            }
        }
        void emailtextChanged(object sender, TextChangedEventArgs e)
        {

            if (SpecialCharacters.Any(e.NewTextValue.Contains))
            {
                newEmail = email.Text;
                newEmail = newEmail.Remove(newEmail.Length - 1);
                email.Text = newEmail;
            }
        }
        /// <summary>
        /// This button sends the entered email and password to ether the ios or android depending which class calls the 
        /// IFirebaseAuthenticator interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void LoginClicked(object sender, EventArgs e)
        {
            string Email = email.Text;
            string Pass = pass.Text;
            if (Email.Contains("@") && Email.Contains("."))
            {
                
            }
            else
            {
                await DisplayAlert("Error", "Please enter a valid email\n", "OK");
                emailCorrect = false;
            }

            string physioUid = await auth.LoginWithEmailPassword(Email, Pass);
            if(physioUid == "true")
            {
                physioUid = await secuirty.checkIfLoginIsVerified(Email, Pass);
            }
            if (physioUid != "")
            {
                await DisplayAlert("Login Successful","", "OK");
                await Navigation.PushAsync(new DisplayExercises(physioUid));
            }
            else
            {
                await DisplayAlert("Login Failed", "E-mail or password are incorrect. Try again!", "OK");
            }
        }
        /// <summary >
        /// This method will bring the physiotherapist registration page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void RegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPhsysio(auth));
        }
    }
}
