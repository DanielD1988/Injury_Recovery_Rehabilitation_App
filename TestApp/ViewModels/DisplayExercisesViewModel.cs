using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.models;
using TestApp.services;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class accesses the exercise functions of the FirebaseMethods class
    /// </summary>
    class DisplayExercisesViewModel
    {
        private FirebaseMethods fire;
        /// <summary>
        /// This constructor calls the eager singleton instance of FirebaseMethods  
        /// </summary>
        public DisplayExercisesViewModel() {
            fire = FirebaseMethods.GetInstance();
        }
        /// <summary>
        /// This method returns all exercise plans from the FirebaseMethods GetAllExercises method
        /// </summary>
        /// <returns></returns>
        public async Task<List<Exercise>> GetExerciseList()
        {
            return await fire.GetAllExercises();
        }
        /// <summary>
        /// This method passses a key to the FirebaseMethods GetExercise() to 
        /// retrive a single exercise plan from a list of exercises
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Exercise> sendExerciseKey(string key)
        {
            Exercise exercise = await fire.GetExercise(key);

            return exercise;
        }
       

    }
}
