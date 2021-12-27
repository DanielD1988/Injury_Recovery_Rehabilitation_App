using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.models;
using TestApp.ViewModels;
using TestApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.views
{
    /// <summary>
    /// This view class is used to display a list of exercises to a mobile screen
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayExercises : ContentPage
    {
        private DisplayExercisesViewModel viewModel;
        private string physioUid = ""
;       /// <summary>
        /// This constructor creates an instance of DisplayExercisesViewModel to call the FirebaseMethods methods
        /// </summary>
        public DisplayExercises(string physioUid)
        {
            viewModel = new DisplayExercisesViewModel();
            this.physioUid = physioUid;
            InitializeComponent();
            
        }
        /// <summary>
        /// This OnAppearing method is used to populate a list view with a list of exercises by calling 
        /// the FirebaseMethods methods through DisplayExercisesViewModel
        /// Linq groupby is used to group exercises togather
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MyListView.ItemsSource = null;
            List<ExercisePlan> exercises = await viewModel.GetExerciseList();
            var sortedByCategorys = exercises.GroupBy(val => val.Category);
            MyListView.ItemsSource = sortedByCategorys;
        }
        /// <summary>
        /// This Details button press method returns with an exercise key and opens a new page called ExerciseDetail
        /// through DisplayExercisesViewModel
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="args"></param>
        public void Details(Object Sender, EventArgs args)
        {
            Button button = (Button)Sender;
            string exerciseKey = button.CommandParameter.ToString().Replace("\"", "");
            Navigation.PushModalAsync(new ExerciseDetail(exerciseKey, physioUid));
        }

    }
}
