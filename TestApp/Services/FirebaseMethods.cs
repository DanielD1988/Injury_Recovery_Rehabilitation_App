using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using TestApp.models;
namespace TestApp.services
{
    /// <summary>
    /// FirebaseMethods class contains methods used to read and write to Firebase realtime database
    /// </summary>
    public class FirebaseMethods
    {
        private static FirebaseClient firebase = new FirebaseClient("https://injuryrecovery-default-rtdb.europe-west1.firebasedatabase.app/");
        private static FirebaseMethods fire = new FirebaseMethods();
        /// <summary>
        /// The constructor is private because the eager singleton pattern is used
        /// </summary>
        private FirebaseMethods()
        {
            
        }
        /// <summary>
        /// This method returns a single instance to all callers of the FirebaseMethods class
        /// </summary>
        /// <returns></returns>
        public static FirebaseMethods GetInstance()
        {
            return fire;
        }
        /// <summary>
        /// This method returns all exercises of a JSON list called exercise
        /// </summary>
        /// <returns></returns>
        //https://xamarinmonkeys.blogspot.com/2019/01/xamarinforms-working-with-firebase.html
        public async Task<List<Exercise>> GetAllExercises()
        {
            try
            {
                return (await firebase
                .Child("exercisePlans")
                .OnceAsync<Exercise>()).Select(item => new Exercise
                {
                    exerciseName = item.Object.exerciseName,
                    exerciseDescription = item.Object.exerciseDescription,
                    ImageBase64 = item.Object.ImageBase64,
                    exerciseInfo = item.Object.exerciseInfo,
                    Category = item.Object.Category,
                    exerciseListKey = item.Key,
                }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null; } 
        }
        /// <summary>
        /// This method returns a single exercise from a list of exercises using an exercise key
        /// </summary>
        /// <param name="exerciseKey"></param>
        /// <returns></returns>
        public async Task<Exercise> GetExercise(String exerciseKey)
        {
            var allExercises = await GetAllExercises();
            await firebase
              .Child("exercisePlans")
              .OnceAsync<Exercise>();
            return allExercises.Where(a => a.exerciseListKey == exerciseKey).FirstOrDefault();
        }///////////////////////////////////////////////////////////////////////////
    }

}
