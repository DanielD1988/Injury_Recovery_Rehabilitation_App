using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using TestApp.models;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.services
{
    /// <summary>
    /// FirebaseMethods class contains methods used to read and write to Firebase realtime database
    /// </summary>
    public class FirebaseMethods
    {
        private static FirebaseClient firebase = new FirebaseClient("https://injuryrecovery-default-rtdb.europe-west1.firebasedatabase.app/");
        private static FirebaseMethods fire = new FirebaseMethods();
        private static List<ExercisePlan> plans = new List<ExercisePlan>();
        private static List<Exercise> exerciseList = new List<Exercise>();
        MockDatabase db = new MockDatabase();
        /// <summary>
        /// The constructor is private as the singleton pattern is used
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
        /// This method returns all exercise plans of a JSON list called exercisePlans
        /// </summary>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        //https://xamarinmonkeys.blogspot.com/2019/01/xamarinforms-working-with-firebase.html
        public async Task<List<ExercisePlan>> GetAllExercises(bool isMocked)
        {
            try
            {
                if (isMocked == true)
                {
                    return db.GetAllMockExercises();
                }
                if (plans.Count == 0)
                {
                    plans = (await firebase
                   .Child("exercisePlans")
                   .OnceAsync<ExercisePlan>()).Select(item => new ExercisePlan
                   {
                       exerciseName = item.Object.exerciseName,
                       exerciseDescription = item.Object.exerciseDescription,
                       ImageBase64 = item.Object.ImageBase64,
                       Category = item.Object.Category,
                       Exercise1 = item.Object.Exercise1,
                       Exercise2 = item.Object.Exercise2,
                       Exercise3 = item.Object.Exercise3,
                       exerciseListKey = item.Key,
                   }).ToList();
                }
                return plans;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// This method returns a single exercise from a list of exercises using an exercise key
        /// </summary>
        /// <param name="exerciseKey"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<ExercisePlan> GetExercise(String exerciseKey, bool isMocked)
        {
            var allExercises = await GetAllExercises(isMocked);
            if (isMocked == true)
            {
                return db.GetMockExercise(exerciseKey);
            }
            await firebase
              .Child("exercisePlans")
              .OnceAsync<ExercisePlan>();
            return allExercises.Where(a => a.exerciseName == exerciseKey).FirstOrDefault();
        }///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This method inserts a new patient entry into the patient database using the patientUid as the key
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="injuryType"></param>
        /// <param name="injuryOccurred"></param>
        /// <param name="age"></param>
        /// <param name="injurySeverity"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="exercise1"></param>
        /// <param name="exercise2"></param>
        /// <param name="exercise3"></param>
        /// <param name="email"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> AddPatient(string patientUid, string name, string gender, string injuryType, string injuryOccurred, int age, int injurySeverity, DateTime startDate, DateTime endDate, string exercise1, string exercise2, string exercise3, string email, bool isMocked)
        {
            if (isMocked == true)
            {
                return db.AddMockPatient(patientUid, name, gender, injuryType, injuryOccurred, age, injurySeverity, startDate, endDate, exercise1, exercise2, exercise3, email);
            }
            try
            {
                await firebase
                .Child("patients").Child(patientUid)
                .PutAsync(new Patient()
                {
                    PatientName = name,
                    Gender = gender,
                    InjuryType = injuryType,
                    InjuryOccurred = injuryOccurred,
                    Age = age,
                    InjurySeverity = injurySeverity,
                    StartDate = startDate,
                    EndDate = endDate,
                    Exer1 = exercise1,
                    Exer2 = exercise2,
                    Exer3 = exercise3,
                    Email = email
                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// This function adds the patient unique identifier to the physios patient list
        /// </summary>
        /// <param name="PhysioUid"></param>
        /// <param name="PatientUid"></param>
        /// /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> AddPatientUIDToPatientList(string PhysioUid, string PatientUID, bool isMocked)
        {
            if (isMocked == true)
            {
                return db.AddMockPatientUIDToPatientList(PhysioUid, PatientUID);
            }
            try
            {
                await firebase
                .Child("physio").Child(PhysioUid).Child("patients").Child(PatientUID)
                .PutAsync(new Physiotherapist()
                { PatientUid = PatientUID });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<List<Exercise>> GetPatientExercises(bool isMocked)
        {
            List<Exercise> allExercises = (await firebase
                   .Child("exercises")
                   .OnceAsync<Exercise>()).Select(item => new Exercise
                   {
                       ExerciseName = item.Object.ExerciseName,
                       ExerciseDescription = item.Object.ExerciseDescription,
                       VideoLink = item.Object.VideoLink
                   }).ToList();

            foreach (Exercise exercise in allExercises)
            {
                if ("Knee Pain" == exercise.ExerciseName)
                {
                    exerciseList.Add(exercise);
                }
            }
            return exerciseList;
        }
        /// <summary>
        /// This method gets an exercise video download link back from firebase storage
        /// </summary>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<string> GetVideosFromStorage(bool isMocked,string videoName)
        {
            FirebaseStorage firebaseStorage = new FirebaseStorage("injuryrecovery.appspot.com");
            var video = await firebaseStorage.Child("video").Child(videoName).GetDownloadUrlAsync();
            string downloadUrl = video.ToString();
            return downloadUrl;
        }
    }
}
