using TestApp.services;
using Xunit;

namespace UnitTestProject
{
    public class FirebaseMethodsTest
    {

        [Fact]
        public void Test1()
        {
            FirebaseMethods fire = new FirebaseMethods();
            Assert.NotNull(fire.GetAllExercises());
        }
    }
}
