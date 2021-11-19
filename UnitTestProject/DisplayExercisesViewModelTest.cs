using System;
using TestApp.ViewModels;
using Xunit;

namespace UnitTestProject
{
    public class DisplayExercisesViewModelTest
    {
        /// <summary>
        /// This test checks if there is data coming from the GetAllExercises() database method 
        /// </summary>
        [Fact]
        public void Test1()
        {
            DisplayExercisesViewModel model = new DisplayExercisesViewModel();
            Assert.NotNull(model.GetExerciseList());
            
        }
    }
}
