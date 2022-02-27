using TestApp.Models;
using TestApp.ViewModels;
using Xunit;

namespace UnitTestProject
{
    public class CurrentPatientViewModelTest
    {
        CurrentPatientViewModel current = new CurrentPatientViewModel();
        [Fact]
        public void getpatientDetailsTest()
        {
            Patient patient =  current.getpatientDetails("1afshwjs", true).Result;
            Assert.NotNull(patient);
        }
    }
}
