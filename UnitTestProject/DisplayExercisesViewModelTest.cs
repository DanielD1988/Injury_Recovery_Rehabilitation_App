using System;
using TestApp.ViewModels;
using Xunit;
/// <summary>
/// This test class test the methods of DisplayExercisesViewModel
/// </summary>
namespace UnitTestProject
{
    public class DisplayExercisesViewModelTest
    {
        DisplayExercisesViewModel model = new DisplayExercisesViewModel();
        /// <summary>
        /// This test checks if there is data coming from the GetAllExercises() database method 
        /// </summary>
        [Fact]
        public void Test1()
        {
            Assert.NotNull(model.GetExerciseList());

        }
        /// <summary>
        /// This test checks if there is data coming from the sendExerciseKey() database method 
        /// </summary>
        [Fact]
        public void Test2()
        {
            Assert.NotNull(model.sendExerciseKey("exe1"));
        }
    }
}
