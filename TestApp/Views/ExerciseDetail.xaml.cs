using System;
using TestApp.models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseDetail : ContentPage
    {
        private DisplayExercisesViewModel viewModel;
        String exerciseKey;
        public ExerciseDetail(String key)
        {
            exerciseKey = key;
            viewModel = new DisplayExercisesViewModel();
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Exercise currentExercise = await viewModel.sendExerciseKey(exerciseKey);
            BindingContext = currentExercise;
        }
        public void AddToPatient(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;
            string exerciseKey = button.CommandParameter.ToString();
            //Navigation.PushModalAsync(new ExerciseDetail(exerciseKey));
        }
    }
}