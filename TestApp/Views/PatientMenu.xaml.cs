using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    /// <summary>
    /// This will be the menu page after the patient login
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientMenu : ContentPage
    {
        public PatientMenu(string uid)
        {
            InitializeComponent(); 
        }
        
    }
}