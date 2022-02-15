using System;
using System.Collections.Generic;
using TestApp.models;
using TestApp.Models;
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
        List<PatientDetails> details = new List<PatientDetails>();
        PlanProgressViewModel model = new PlanProgressViewModel();
        String exerciseKey;
        string physioUid = "";
        int minimum1 = 0;
        int maximum1 = 0;
        int minimum2 = 0;
        int maximum2 = 0;
        int minimum3 = 0;
        int maximum3 = 0;
        ExercisePlan currentExercise = null;
        string errorMessage = "The min value cannot be larger than the max value";
        bool isCorrect = true;
        bool isExer1Correct = false;
        bool isExer2Correct = false;
        bool isExer3Correct = false;
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
        public async void createPatient(Object Sender, EventArgs args)
        {
            if (allMinMaxValuesFilledIn())
            {
                checkMinMaxValuesAreCorrect();
                if (isCorrect && isExer1Correct && isExer2Correct && isExer3Correct)
                {
                    await Navigation.PushModalAsync(new RegisterPatient(currentExercise, physioUid, minimum1, minimum2, minimum3, maximum1, maximum2, maximum3));
                }
                else
                {
                    await DisplayAlert("Error", errorMessage, "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please fill in all min max values for each exercise\n", "OK");
            }
        }
        public async void selectPatient(Object Sender, EventArgs args)
        {

            if (allMinMaxValuesFilledIn())
            {
                checkMinMaxValuesAreCorrect();
                if (isCorrect && isExer1Correct && isExer2Correct && isExer3Correct)
                {
                    details = await model.getPatientNameAndPatientUserId(physioUid);
                    await Navigation.PushModalAsync(new AddToExistingPatient(currentExercise, physioUid, minimum1, minimum2, minimum3, maximum1, maximum2, maximum3, details));
                }
                else
                {
                    await DisplayAlert("Error", errorMessage, "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please fill in all min max values for each exercise\n", "OK");
            }
        }
        /// <summary>
        /// This function calls the checkIfMinLessThanMax method with the three differnt exercises
        /// </summary>
        public void checkMinMaxValuesAreCorrect()
        {
            isExer1Correct = checkIfMinLessThanMax(minimum1, maximum1, exercise1.Text);
            isExer2Correct = checkIfMinLessThanMax(minimum2, maximum2, exercise2.Text);
            isExer3Correct = checkIfMinLessThanMax(minimum3, maximum3, exercise3.Text);
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
        /// <summary>
        /// This method checks if the min value is less than the max value for the exercise
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="exerciseName"></param>
        /// <returns></returns>
        private bool checkIfMinLessThanMax(int min,int max,string exerciseName)
        {
            if(min > max)
            {
                return false;
            }
            return true;
            
        }
    }
}