using System;
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
        public void GetExerciseTest()
        {
            Assert.NotNull(fire.GetExercise("Knee pain",true));
        }
        /// <summary>
        /// This test checks if a patients details were added into the database
        /// </summary>
        [Fact]
        public void AddPatientTest()
        {
            Assert.True(fire.AddPatient("000123fr", "Bill hazard", "male", "sprain", "gym", 44, 3, DateTime.Now, DateTime.Now, "Knee Pain", "BillH@gmail.com",true).Result);
        }

        [Fact]
        public void AddPatientUIDToPatientList()
        {
            Assert.True(fire.AddPatientUIDToPatientList("adcd4321", "000123fr",true).Result);
        }
    }
}
