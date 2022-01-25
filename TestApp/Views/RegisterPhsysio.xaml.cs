using System;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;

namespace TestApp.Views
{
    /// <summary>
    /// This class will allow a physiotherapist to sign up to the application
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPhsysio : ContentPage
    {
        IFirebaseAuthenticator auth;
        private Regex r = new Regex("^[a-zA-Z0-9]*$");
        private Regex p = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$^&*_]).{8,}$");//atleast one upper case char one number and one of theses special chars #?!@$^&*_
        private bool isPasswordConfirmed = false;
        private string enteredEmail = "";
        private bool isPaymentConfirmed = false;
        public RegisterPhsysio(IFirebaseAuthenticator Auth)
        {
            auth = Auth;
            InitializeComponent();
        }

        void checkName(object sender, TextChangedEventArgs e)
        {
            if (r.IsMatch(e.NewTextValue))
            {
                name.Text = e.NewTextValue;
            }
        }
        void checkPass(object sender, TextChangedEventArgs e)
        {
            if (r.IsMatch(e.NewTextValue))
            {
                pass.Text = e.NewTextValue;
            }
        }
        bool checkToConfirmPass()
        {
            if (pass.Text.Equals(conPass))
            {
                return true;
            }
                return false;
        }
        bool checkIfValidEmail(string email)
        {
            if(email.Contains("@") && email.Contains("."))
            {
                return true;
            }
            return false;
        }

        async void payment(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Payment());
        }
        async void registerPhysio(object sender, EventArgs e)
        {
            enteredEmail = email.Text;
            bool emailOk = checkIfValidEmail(enteredEmail);
            isPasswordConfirmed = checkToConfirmPass();

            if (emailOk == true && isPasswordConfirmed == true && isPaymentConfirmed == true)
            {
                string eneteredName = name.Text;
                string eneteredPassword = pass.Text;
                string token = await auth.SignupWithEmailPassword(enteredEmail, eneteredPassword);
                await DisplayAlert("Logged in", "Token: " + token, "Ok");
            }
        }
    }
}