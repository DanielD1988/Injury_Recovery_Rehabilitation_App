using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
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
        /// <returns></returns>
        //https://xamarinmonkeys.blogspot.com/2019/01/xamarinforms-working-with-firebase.html
        public async Task<List<ExercisePlan>> GetAllExercises(bool isMocked)
        {
            try
            {
                if(isMocked == true)
                {
                    return db.GetAllMockExercises();
                }
                if(plans.Count == 0)
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
                return null; } 
        }
        /// <summary>
        /// This method returns a single exercise from a list of exercises using an exercise key
        /// </summary>
        /// <param name="exerciseKey"></param>
        /// <returns></returns>
        public async Task<ExercisePlan> GetExercise(String exerciseKey,bool isMocked)
        {
            var allExercises = await GetAllExercises(isMocked);
            if(isMocked == true)
            {
                return db.GetMockExercise(exerciseKey);
            }
            await firebase
              .Child("exercisePlans")
              .OnceAsync<ExercisePlan>();
            return allExercises.Where(a => a.exerciseListKey == exerciseKey).FirstOrDefault();
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
        /// <param name="exerPlan"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> AddPatient(string patientUid,string name, string gender,string injuryType,string injuryOccurred,int age,int injurySeverity, DateTime startDate, DateTime endDate ,string exerPlan,string email,bool isMocked)
        {
            if (isMocked == true)
            {
                return db.AddMockPatient(patientUid, name, gender, injuryType, injuryOccurred, age, injurySeverity, startDate, endDate, exerPlan, email);
            }
            try
            {
                await firebase
                .Child("patients").Child(patientUid)
                .PutAsync(new Patient() { 
                     PatientName = name, 
                     Gender = gender, InjuryType = injuryType, 
                     InjuryOccurred = injuryOccurred, 
                     Age = age, 
                     InjurySeverity = injurySeverity, 
                     StartDate = startDate, 
                     EndDate = endDate, 
                     ExerPlan = exerPlan, 
                     Email = email });
                return true;
            }
            catch(Exception e)
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
        /// <returns></returns>
        public async Task<bool> AddPatientUIDToPatientList(string PhysioUid,string PatientUID,bool isMocked)
        {
            if(isMocked == true)
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
    }
}
