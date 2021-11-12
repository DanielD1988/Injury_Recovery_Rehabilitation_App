using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using TestApp.models;
namespace TestApp.services
{
    class FirebaseMethods
    {
        FirebaseClient firebase = new FirebaseClient("https://injuryrecovery-default-rtdb.europe-west1.firebasedatabase.app/");


        public async Task<List<Exercise>> GetAllExercises()
        {
            try
            {
                return (await firebase
             .Child("exercises").Child("Exercise1")
             .OnceAsync<Exercise>()).Select(item => new Exercise
             {
                 exerciseName = item.Object.exerciseName,
                 exerciseDescription = item.Object.exerciseDescription

             }).ToList();
            }
            catch(Exception e)
            {
                return new List<Exercise>();
            }
           
        }
    }

}
