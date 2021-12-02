using System;
using TestApp.ViewModels;
using TestApp.views;
using TestApp.Views;
using Xamarin.Forms;
namespace TestApp
{
    public partial class MainPage : ContentPage
    {
        IFirebaseAuthenticator auth;
        /// <summary>
        /// This is the login page for both apps
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            Navigation.PushModalAsync(new DisplayExercises());
            //https://github.com/xamarin/GooglePlayServicesComponents/issues/391
            auth = DependencyService.Get<IFirebaseAuthenticator>();
            /////////////////////////////////////////////////////////////////////
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

            string Token = await auth.LoginWithEmailPassword(Email, Pass);
            
            if (Token != "")
            {
                await DisplayAlert("Login Successful", Token, "OK");
                await Navigation.PushAsync(new DisplayExercises());
            }
            else
            {
                await DisplayAlert("Login Failed", "E-mail or password are incorrect. Try again!", "OK");
            }
        }
        /// <summary>
        /// This method will bring the user to registration page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void RegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterUser(auth));
        }
    }
}
