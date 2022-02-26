using System.Collections.Generic;
using TestApp.Models;
using TestApp.ViewModels;
using Xunit;

namespace UnitTestProject
{
    public class DisplayPatientExercisePlanViewModelTest
    {
        private DisplayPatientExercisePlanViewModel display = new DisplayPatientExercisePlanViewModel();
        /// <summary>
        /// This test checks to see if a list of exercises is returned
        /// </summary>
        [Fact]
        public void getPatientExercisesTest()
        {
            List<Exercise> result = display.getPatientExercises("squat", "hamstring stretch", "heel and calf stretch", true).Result;
        }
        /// <summary>
        /// This test checks to see if the time is removed from the date
        /// </summary>
        [Fact]
        public void removeTimeFromDateTest()
        {
            string result = display.removeTimeFromDate("16/01/2022 12:00:00");

        }
        /// <summary>
        /// This test checks if a exercise state is saved for a patient
        /// </summary>
        [Fact]
        public void saveStateOfExercisePlanTest()
        {
            bool result = display.saveStateOfExercisePlan("5FBY78XCES", "16/01/2022 12:00:00").Result;

        }
    }
}
