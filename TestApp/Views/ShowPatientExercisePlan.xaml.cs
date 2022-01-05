using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowPatientExercisePlan : ContentPage
    {
        DisplayPatientExercisesViewModel display;
        string downLoadLink1 = "";
        string downLoadLink2 = "";
        string downLoadLink3 = "";
        public ShowPatientExercisePlan()
        {
            display = new DisplayPatientExercisesViewModel();
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            downLoadLink1 =  await display.getExerciseVideoLink("", false);
        }
    }
}