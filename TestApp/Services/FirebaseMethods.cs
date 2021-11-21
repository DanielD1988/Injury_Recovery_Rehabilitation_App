using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using TestApp.models;
namespace TestApp.services
{
    public class FirebaseMethods
    {
        private static FirebaseClient firebase = new FirebaseClient("https://injuryrecovery-default-rtdb.europe-west1.firebasedatabase.app/");
        private static FirebaseMethods fire = new FirebaseMethods();
        private FirebaseMethods()
        {
            
        }
        public static FirebaseMethods GetInstance()
        {
            return fire;
        }
        public async Task<List<Exercise>> GetAllExercises()
        {
            try
            {
                return (await firebase
                .Child("exercise")
                .OnceAsync<Exercise>()).Select(item => new Exercise
                {
                    exerciseName = item.Object.exerciseName,
                    exerciseDescription = item.Object.exerciseDescription,
                    ImageBase64 = item.Object.ImageBase64,
                    exerciseInfo = item.Object.exerciseInfo,
                    exerciseListKey = item.Key,
                }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null; } 
        }

        public async Task<Exercise> GetExercise(String exerciseKey)
        {
            var allExercises = await GetAllExercises();
            await firebase
              .Child("exercise")
              .OnceAsync<Exercise>();
            return allExercises.Where(a => a.exerciseListKey == exerciseKey).FirstOrDefault();
        }
    }

}
