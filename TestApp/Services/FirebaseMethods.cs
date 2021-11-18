using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using TestApp.models;
namespace TestApp.services
{
    public class FirebaseMethods
    {
        FirebaseClient firebase;
        public FirebaseMethods()
        {
            firebase = new FirebaseClient("https://injuryrecovery-default-rtdb.europe-west1.firebasedatabase.app/");
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
                    exerciseListKey = item.Key,
                }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null; } 
        }
        public async Task<Exercise> GetExercise()
        {
            var allExercises = await GetAllExercises();
            await firebase
              .Child("exercise")
              .OnceAsync<Exercise>();
            return allExercises.Where(a => a.exerciseName == "pushup").FirstOrDefault();
        }
    }

}
