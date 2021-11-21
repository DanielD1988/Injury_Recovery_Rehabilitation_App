using System;
using TestApp.ViewModels;
using TestApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayExercises : ContentPage
    {
        private DisplayExercisesViewModel viewModel;

        public DisplayExercises()
        {
            viewModel = new DisplayExercisesViewModel();
            InitializeComponent();
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MyListView.ItemsSource = null;
            MyListView.ItemsSource = await viewModel.GetExerciseList();
        }
        public void Details(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;
            string exerciseKey = button.CommandParameter.ToString();
            Navigation.PushModalAsync(new ExerciseDetail(exerciseKey));
        }

    }
}
