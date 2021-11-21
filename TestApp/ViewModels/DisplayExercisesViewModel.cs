using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.models;
using TestApp.services;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    class DisplayExercisesViewModel : ContentPage
    {
        private FirebaseMethods fire;
        public DisplayExercisesViewModel() {
            fire = FirebaseMethods.GetInstance();
        }

        public async Task<List<Exercise>> GetExerciseList()
        {
            return await fire.GetAllExercises();
        }

        public async Task<Exercise> sendExerciseKey(string key)
        {
            Exercise exercise = await fire.GetExercise(key);

            return exercise;
        }
       

    }
}
