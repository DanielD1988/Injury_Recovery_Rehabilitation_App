﻿using System;
using System.Collections.Generic;
using System.IO;
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
    /// FirebaseMethods class contains methods used to read and write to the Firebase realtime database
    /// </summary>
    public class FirebaseMethods
    {
        private static FirebaseClient firebase = new FirebaseClient("https://injuryrecovery-default-rtdb.europe-west1.firebasedatabase.app/");
        private FirebaseStorage firebaseStorage = new FirebaseStorage("injuryrecovery.appspot.com");
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
        }
        /// <summary>
        /// This method inserts a new patient entry into the patient database using the patient user id as the key
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="injuryType"></param>
        /// <param name="injuryOccurred"></param>
        /// <param name="age"></param>
        /// <param name="injurySeverity"></param>
        /// <param name="exercise1"></param>
        /// <param name="exercise2"></param>
        /// <param name="exercise3"></param>
        /// <param name="email"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> AddPatient(string patientUid, string name, string gender, string injuryType, string injuryOccurred, int age, int injurySeverity, string exercise1, string exercise2, string exercise3, string email,int min1,int min2,int min3,int max1,int max2,int max3, bool isMocked)
        {
            if (isMocked == true)
            {
                return db.AddMockPatient(patientUid, name, gender, injuryType, injuryOccurred, age, injurySeverity,exercise1, exercise2, exercise3, email);
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
                    Exer1 = exercise1,
                    minExercise1 = min1,
                    maxExercise1 = max1,
                    Exer2 = exercise2,
                    minExercise2 = min2,
                    maxExercise2 = max2,
                    Exer3 = exercise3,
                    minExercise3 = min3,
                    maxExercise3 = max3,
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
        /// This method inserts a new physio into the physio database
        /// </summary>
        /// <param name="physioUid"></param>
        /// <param name="name"></param>
        /// <param name="physioIdNumber"></param>
        /// <param name="physioEmail"></param>
        /// <param name="membership"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> AddPhysio(string physioUid, string name,string physioIdNumber, string physioEmail,string membership,bool isMocked)
        {
            if (isMocked == true)
            {
                
            }
            try
            {
                await firebase
                .Child("physio").Child(physioUid)
                .PutAsync(new Physiotherapist()
                {
                    PhysioName = name,
                    Email = physioEmail,
                    IdNumber = physioIdNumber,
                    Membership = membership
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
        /// This function adds the patient unique identifier and name to the physios patient list
        /// </summary>
        /// <param name="PhysioUid"></param>
        /// <param name="PatientUid"></param>
        /// /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> AddPatientUIDToPatientList(string PhysioUid, string PatientUID,string patientName, bool isMocked)
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
                { PatientUid = PatientUID,PatientName = patientName });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        public async Task<List<PatientDetails>> GetNamesAndPatientUids(string PhysioUid, bool isMocked)
        {
            try
            {
                if (isMocked == true)
                {
                    return null;
                }
                else
                {
                    List<PatientDetails> value = (await firebase
                   .Child("physio").Child(PhysioUid).Child("patients")
                   .OnceAsync<PatientDetails>()).Select(item => new PatientDetails
                   {
                       Name = item.Object.Name,
                       Uid = item.Object.Uid

                   }).ToList();

                    return value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// This method adds assigns a  patient or physio type of user to the user id
        /// </summary>
        /// <param name="userUid"></param>
        /// <param name="userType"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> addUserType(string userUid,string userType ,bool isMocked)
        {
            if (isMocked == true)
            {
                
            }
            try
            {
                await firebase
                .Child("CheckUser").Child(userUid)
                .PutAsync(new CheckUser()
                { User = userType });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method saves all the exercise plan dates for a patient
        /// </summary>
        /// <param name="PatientUID"></param>
        /// <param name="isMocked"></param>
        /// <param name="dates"></param>
        /// <returns></returns>
        public async Task<bool> recordPatientProgress(string PatientUID, Dictionary<string, bool> dates,bool isMocked)
        {
            if (isMocked == true)
            {
                return false;
            }
            try
            {
                await firebase
                .Child("patientProgress").Child(PatientUID)
                .PutAsync(new Progress()
                { planDates = dates });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method updates the value set at a date of the patients plan 
        /// </summary>
        /// <param name="PatientUID"></param>
        /// <param name="date"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, bool>> getPatientProgress(string PatientUID, bool isMocked)
        {

            if (isMocked == true)
            {
                return null;
            }
            try
            {
                var value = await firebase.Child("patientProgress").Child(PatientUID).OnceSingleAsync<Progress>();
                return value.planDates;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        
        /// <summary>
        /// This method uses the patient user id and returns information about the patient 
        /// including what exercises have been assigned to them for their rehabilitation
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<Patient> getpatientDetails(string patientUid, bool isMocked)
        {
            if (isMocked == true)
            {
                return null;
            }
            try
            {
                return await firebase
               .Child("patients").Child(patientUid).OnceSingleAsync<Patient>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This method returns what type of user is trying to access the app
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<CheckUser> getTypeOfUser(string userId, bool isMocked)
        {
            if (isMocked == true)
            {
                return null;
            }
            try
            {
                return await firebase
               .Child("CheckUser").Child(userId).OnceSingleAsync<CheckUser>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This function gets a list of exercises from the database then
        /// the three entered exercise strings are used to build a list that contains the exercise plan for the patient
        /// </summary>
        /// <param name="exercise1"></param>
        /// <param name="exercise2"></param>
        /// <param name="exercise3"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<List<Exercise>> GetPatientExercises(string exercise1, string exercise2, string exercise3 ,bool isMocked)
        {
            if (isMocked == true)
            {
                return null;
            }
            try
            {
                List<Exercise> allExercises = (await firebase
                   .Child("exercises")
                   .OnceAsync<Exercise>()).Select(item => new Exercise
                   {
                       ExerciseName = item.Object.ExerciseName,
                       exerciseVideoCopyright = item.Object.exerciseVideoCopyright,
                       VideoLink = item.Object.VideoLink
                   }).ToList();

                foreach (Exercise exercise in allExercises)
                {
                    if (exercise1 == exercise.ExerciseName || exercise2 == exercise.ExerciseName || exercise3 == exercise.ExerciseName)
                    {
                        exerciseList.Add(exercise);
                    }
                }
                return exerciseList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This method gets an exercise video download link back from firebase storage
        /// </summary>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<string> GetVideosFromStorage(bool isMocked,string videoName)
        {
            if (isMocked == true)
            {
                return null;
            }
            try
            {
                var video = await firebaseStorage.Child("video").Child(videoName).GetDownloadUrlAsync();
                string downloadUrl = video.ToString();
                return downloadUrl;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This method gets an image from firebase storage
        /// </summary>
        /// <param name="isMocked"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public async Task<string> GetImageFromStorage(bool isMocked, string imageName)
        {
            if (isMocked == true)
            {
                return null;
            }
            try
            {
                var image = await firebaseStorage.Child("pics").Child(imageName).GetDownloadUrlAsync();
                string downloadUrl = image.ToString();
                return downloadUrl;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// This method saves an image to firebase storage
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<bool> SaveImageToStorage(Stream image,bool isMocked)
        {
            if (isMocked == true)
            {
                return true;
            }
            try
             {
                await firebaseStorage
                 .Child("pics")
                 .PutAsync(image);

                 return true;
             }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// This method works as a replacement for firebases SignupWithEmailPassword for iOS
        /// </summary>
        /// <param name="email"></param>
        /// <param name="salted"></param>
        /// <param name="saltedAndHashed"></param>
        /// <param name="uid"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> iOSSignupWithEmailPassword(string email, string salted, string saltedAndHashed,string uid,bool isMocked)
        {
            if (isMocked == true)
            {
                return true;
            }
            try
            {
                email = email.Replace("@", "");
                email = email.Replace(".", "");
                await firebase
                .Child("iosCredentials").Child(email)
                .PutAsync(new IosCredentials()
                {
                    userId = uid,
                    salt = salted,
                    saltHashed = saltedAndHashed
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
        /// This method gets the salt the saltedAndHashed password and user id back from a valid email address
        /// </summary>
        /// <param name="isMocked"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<IosCredentials> getIosPatientPasswordDetails(bool isMocked, string email)
        {
            email = email.Replace("@", "");
            email = email.Replace(".", "");
            if (isMocked == true)
            {
                return null;
            }
            try
            {
                return await firebase.Child("iosCredentials").Child(email).OnceSingleAsync<IosCredentials>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
