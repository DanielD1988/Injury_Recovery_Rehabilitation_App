using TestApp.ViewModels;
using Xunit;
/// <summary>
/// This test class test the methods of DisplayExercisesViewModel
/// </summary>
namespace UnitTestProject
{
    //https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
    public class DisplayExercisesViewModelTest
    {
        DisplayExercisesViewModel model = new DisplayExercisesViewModel();
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
        public void sendExerciseKeyTest2()
        {
            Assert.NotNull(model.sendExerciseKey("Knee pain",true));
        }
    }
}
