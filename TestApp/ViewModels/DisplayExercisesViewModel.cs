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
            fire = new FirebaseMethods();
        }

        public async Task<List<Exercise>> GetExerciseList()
        {
            return await fire.GetAllExercises();
        }
       

    }
}
