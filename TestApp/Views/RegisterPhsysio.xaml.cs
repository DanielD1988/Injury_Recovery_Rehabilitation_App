using System;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This class will allow a physiotherapist to sign up to the application
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPhsysio : ContentPage
    {
        IFirebaseAuthenticator auth;
        public RegisterPhsysio(IFirebaseAuthenticator Auth)
        {
            auth = Auth;
            InitializeComponent();
        }
        async void confirmed(object sender, EventArgs e)
        {
            string Email = email.Text;
            string Pass = pass.Text;

            string token = await auth.SignupWithEmailPassword(Email, Pass);
            await DisplayAlert("Logged in", "Token: " + token, "Ok");
        }
    }
}