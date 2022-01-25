using System;
using TestApp.models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This ExerciseDetail page shows a detailed description of the exercise
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseDetail : ContentPage
    {
        private DisplayExercisePlansViewModel viewModel;
        String exerciseKey;
        string physioUid = "";
        int minimum1 = 0;
        int maximum1 = 0;
        int minimum2 = 0;
        int maximum2 = 0;
        int minimum3 = 0;
        int maximum3 = 0;
        ExercisePlan currentExercise = null;
        /// <summary>
        ///  This constructor takes the exercise key from DisplayExercises Detail button press
        ///  and creates an instance of DisplayExercisesViewModel to access the FirebaseMethods class methods
        /// </summary>
        /// <param name="key"></param>
        /// <param name="physioUid"></param>
        public ExerciseDetail(String key, string physioUid)
        {
            exerciseKey = key;
            viewModel = new DisplayExercisePlansViewModel();
            this.physioUid = physioUid;
            InitializeComponent();
        }
        /// <summary>
        /// This OnAppearing method is used to populate the stack view with the returned exercise details by using the
        /// exercise key
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            currentExercise = await viewModel.sendExerciseKey(exerciseKey, false);
            BindingContext = currentExercise;
        }
        /// <summary>
        /// This AddToPatient button passes the exerciseKey and opens a new page called sendEmailToPatient
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        public async void AddToPatient(Object Sender, EventArgs args)
        {
            if (allMinMaxValuesFilledIn())
            {
                await Navigation.PushModalAsync(new RegisterPatient(currentExercise, physioUid,minimum1,minimum2,minimum3,maximum1,maximum2,maximum3));
            }
            else
            {
                await DisplayAlert("Error", "Please fill in all min max values for each exercise\n", "OK");
            }
        }
        /// <summary>
        /// This method checks to see if all min max values are assigned to all exercises in the plan
        /// </summary>
        /// <returns></returns>
        public bool allMinMaxValuesFilledIn()
        {
            bool allNumbersAdded = true;
            allNumbersAdded = int.TryParse(min1.Text, out minimum1);
            allNumbersAdded = int.TryParse(max1.Text, out maximum1);
            allNumbersAdded = int.TryParse(min2.Text, out minimum2);
            allNumbersAdded = int.TryParse(max2.Text, out maximum2);
            allNumbersAdded = int.TryParse(min3.Text, out minimum3);
            allNumbersAdded = int.TryParse(max3.Text, out maximum3);
            return allNumbersAdded;
        }
    }
}