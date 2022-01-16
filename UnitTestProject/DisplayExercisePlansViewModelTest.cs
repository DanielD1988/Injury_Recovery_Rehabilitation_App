using TestApp.ViewModels;
using Xunit;
/// <summary>
/// This test class test the methods of DisplayExercisesViewModel
/// </summary>
namespace UnitTestProject
{
    //https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
    public class DisplayExercisePlansViewModelTest
    {
        DisplayExercisePlansViewModel model = new DisplayExercisePlansViewModel();
        /// <summary>
        /// This test checks if there is data coming from the GetAllExercises() database method 
        /// </summary>
        [Fact]
        public void getExerciseListTest()
        {
            Assert.NotNull(model.getExerciseList(true));

        }
        /// <summary>
        /// This test checks if there is data coming from the sendExerciseKey() database method 
        /// </summary>
        [Fact]
        public void sendExerciseKeyTest1()
        {
            Assert.NotNull(model.sendExerciseKey("Knee pain",true));
        }
        /// <summary>
        /// This test checks if there is data coming from the sendExerciseKey() database method when no plan is given
        /// as expected the test will fail
        /// </summary>
        [Fact]
        public void sendExerciseKeyTest2()
        {
            var value = model.sendExerciseKey("", true).Result;
            Assert.Null(value);
        }
    }
}
