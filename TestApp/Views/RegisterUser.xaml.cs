using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUser : ContentPage
    {
        IFirebaseAuthenticator auth;
        public RegisterUser(IFirebaseAuthenticator Auth)
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