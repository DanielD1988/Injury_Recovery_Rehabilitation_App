using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //BindingContext = new DisplayExercisesViewModel();
            Exercise currentExercise = await viewModel.sendExerciseKey(exerciseKey);
            BindingContext = currentExercise;

        }
    }
}