using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PredictInjuryType : ContentPage
    {
        string sLevel = null;
        string gender = null;
        string stringAge = null;
        string isInjuryBefore = null;
        string selectedActivity = null;
        private RadioButton genderButton, injuryButton;

        public PredictInjuryType()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Picker for skill level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skillLevel(object sender, EventArgs e)
        {
            if (skill.SelectedIndex != -1)
            {
                sLevel = skill.SelectedItem.ToString();
            }
        }
        /// <summary>
        /// Radio button for gender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getGender(object sender, CheckedChangedEventArgs e)
        {
            genderButton = sender as RadioButton;
            gender = genderButton.Content.ToString();
        }
        /// <summary>
        /// Picker for age
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAge(object sender, EventArgs e)
        {
            if (age.SelectedIndex != -1)
            {
                stringAge = age.SelectedItem.ToString();
            }
        }
        /// <summary>
        /// Radio button previous injury
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getInjuryBefore(object sender, CheckedChangedEventArgs e)
        {
            injuryButton = sender as RadioButton;
            isInjuryBefore = injuryButton.Content.ToString();
        }
        /// <summary>
        /// Picker for patient activity eg Rugby
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectActivity(object sender, EventArgs e)
        {
            if (activity.SelectedIndex != -1)
            {
                selectedActivity = activity.SelectedItem.ToString();
            }
        }
        /// <summary>
        /// Send values to view model for processing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void sendValues(object sender, EventArgs e)
        {
            if(sLevel != null && gender != null && stringAge != null && isInjuryBefore != null && selectedActivity != null)
            {
                await DisplayAlert("Success", "data sent", "OK");
                //send values to view modal and then to api call
            }
            else
            {
                await DisplayAlert("Error","You did not select all the values", "OK");
            }
        }
    }
}