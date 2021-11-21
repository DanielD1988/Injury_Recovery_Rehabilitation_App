using TestApp.services;
using Xunit;

namespace UnitTestProject
{
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
        public void Test1()
        {
            Assert.NotNull(fire.GetAllExercises());
        }
        /// <summary>
        /// This test checks to see if there is a single exercises being returned
        /// </summary>
        [Fact]
        public void Test2()
        {
            Assert.NotNull(fire.GetExercise("exe1"));
        }
    }
}
