using System.Collections.Generic;
using TestApp.Models;
using TestApp.services;
using Xunit;

namespace UnitTestProject
{
    //https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
    /// <summary>
    /// This test class tests the methods of the FirebaseMethods class
    /// </summary>
    public class FirebaseMethodsTest
    {
        FirebaseMethods fire = FirebaseMethods.GetInstance();
        /// <summary>
        /// This test checks to see if there is a list of exercises being returned
        /// </summary>
        [Fact]
        public void GetAllExercisesTest()
        {
            Assert.NotNull(fire.GetAllExercises(true));
        }
        /// <summary>
        /// This test checks to see if there is a single exercises being returned
        /// </summary>
        [Fact]
        public void GetExerciseTest1()
        {
            Assert.NotNull(fire.GetExercise("Knee pain",true));
        }
        /// <summary>
        /// This test checks to see if there is a single exercises being returned it will fail 
        /// as no exercise name is entered
        /// </summary>
        [Fact]
        public void GetExerciseTest2()
        {
            Assert.Null(fire.GetExercise("", true).Result);
        }
        /// <summary>
        /// This test checks if a patients details were added into the database
        /// </summary>
        [Fact]
        public void AddPatientTest1()
        {
            //Assert.True(fire.AddPatient("000123fr", "Bill hazard", "male", "sprain", "gym", 44, 3,"exer1","exer2","exer2", "BillH@gmail.com",true).Result);
        }
        /// <summary>
        /// This test checks if a patients details were added into the database it will fail as no patient name is added
        /// </summary>
        [Fact]
        public void AddPatientTest2()
        {
            //Assert.False(fire.AddPatient("000123fr", null, "male", "sprain", "gym", 44, 3, "exer1", "exer2", "exer2", "BillH@gmail.com", true).Result);
        }
        /// <summary>
        /// Checks if the the patient user id is added to the list of patient ids
        /// </summary>
        [Fact]
        public void AddPatientUIDToPatientListTest()
        {
            //Assert.True(fire.AddPatientUIDToPatientList("adcd4321", "000123fr",true).Result);
        }
        /// <summary>
        /// This test checks to see if the patients exercise plan is saved
        /// </summary>
        [Fact]
        public void recordPatientProgressTest()
        {
            //bool value = fire.recordPatientProgress("123ABFET", true, "2022-01-14").Result;
        }

        /// <summary>
        /// This test checks to see if a patient is returned by using a valid patient user id
        /// </summary>
        [Fact]
        public void getpatientDetailsTest()
        {
            Patient value = fire.getpatientDetails("14gftrs2", true).Result;
        }
        /// <summary>
        /// This test returns the exercises assigned to a patient
        /// </summary>
        [Fact]
        public void GetPatientExercisesTest()
        {
            List<Exercise> value = fire.GetPatientExercises("squat","hamstring stretch","heel and calf stretch",true).Result;
        }
        /// <summary>
        /// This function retruns a download link to a video from firebase storage
        /// </summary>
        [Fact]
        public void GetVideosFromStorageTest()
        {
            string value = fire.GetVideosFromStorage(true, "hamstringStretch.mp4").Result;
        }
        /// <summary>
        /// This test checks to see if a new user can signup to the app 
        /// </summary>
        [Fact]
        public void iOSSignupWithEmailPasswordTest()
        {
            bool value = fire.iOSSignupWithEmailPassword("john@hotmail.com","3F5HVSFD76BN","CKOERT673NYEK35NF","VIHDBSJQ",true).Result;
        }
        /// <summary>
        /// This test checks to see if the email entered is valid and data associated with it is returned
        /// </summary>
        [Fact]
        public void getIosPatientPasswordDetailsTest()
        {
            IosCredentials value = fire.getIosPatientPasswordDetails(true,"john@hotmail.com").Result;
        }
    }
}
