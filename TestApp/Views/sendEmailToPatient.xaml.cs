using System;
using System.Collections.Generic;
using TestApp.ViewModels;
using TestApp.views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This class is used to send login info to a patient
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class sendEmailToPatient : ContentPage
    {
        public sendEmailToPatient(String exerciseKey)
        {
            InitializeComponent();

        }
        /// <summary>
        /// This button when pressed will return a physiotherapist back to the previous screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSend_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new DisplayExercises());
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            GenerateEmail email = new GenerateEmail();
            await email.SendEmail(new List<string>());
            await Navigation.PushModalAsync(new DisplayExercises());
        }
    }
}