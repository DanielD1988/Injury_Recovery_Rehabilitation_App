using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientMenu : ContentPage
    {
        public PatientMenu(string uid)
        {
            InitializeComponent(); 
        }
        
    }
}