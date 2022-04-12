using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.models;
using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPlans : ContentPage
    {
        string physiouId = "";
        List<Exercise> exerciseList;
        List<ExercisePlan> physioPlans;
        ExercisePlan selectdPlan;
        ExerciseViewModel vm = new ExerciseViewModel();
        public SelectPlans(string physiouid, List<Exercise> exerciseList, List<ExercisePlan> physioPlans)
        {
            InitializeComponent();
            physiouId = physiouid;
            this.exerciseList = exerciseList;
            this.physioPlans = physioPlans;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<string> names = new List<string>();
            foreach (ExercisePlan plan in physioPlans)
            {
                names.Add(plan.exerciseName);
            }
            namesPicker.ItemsSource = names;
        }
        private async void editPlan(object sender, EventArgs args)
        {
            if (getExercisePlanFromPicker())
            {
                await Navigation.PushModalAsync(new AddPlan(physiouId, exerciseList, selectdPlan, "Edit Plan"));
            }
        }
        private async void deletePlan(object sender, EventArgs args)
        {
            if (getExercisePlanFromPicker())
            {
                await vm.deletePlanToDb(physiouId,selectdPlan.exerciseName);
                await Navigation.PopModalAsync();
            }
        }
        /// <summary>
        /// This method assigns an exercise plan from a list of exercise plans
        /// </summary>
        /// <returns></returns>
        public bool getExercisePlanFromPicker()
        {
            if (namesPicker.SelectedIndex != -1)
            {
                foreach (ExercisePlan plan in physioPlans)
                {
                    if (plan.exerciseName == namesPicker.SelectedItem.ToString())
                    {
                        selectdPlan = plan;
                    }
                }
                return true;
            }
            return false;
        }
    }
}