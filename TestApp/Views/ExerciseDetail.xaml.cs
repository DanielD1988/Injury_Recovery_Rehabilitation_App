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
        private DisplayExercisesViewModel viewModel;
        String exerciseKey;
        /// <summary>
        /// This constructor takes the exercise key from DisplayExercises Detail button press
        /// and creates an instance of DisplayExercisesViewModel to access the FirebaseMethods class methods
        /// </summary>
        /// <param name="key"></param>
        public ExerciseDetail(String key)
        {
            exerciseKey = key;
            viewModel = new DisplayExercisesViewModel();
            InitializeComponent();
        }
        /// <summary>
        /// This OnAppearing method is used to populate the stack view with the returned exercise details by using the
        /// exercise key
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ExercisePlan currentExercise = await viewModel.sendExerciseKey(exerciseKey);
            BindingContext = currentExercise;
        }
        /// <summary>
        /// This AddToPatient button passes the exerciseKey and opens a new page called sendEmailToPatient
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        public void AddToPatient(Object Sender, EventArgs args)
        {
            Navigation.PushModalAsync(new sendEmailToPatient(exerciseKey));
        }
    }
}