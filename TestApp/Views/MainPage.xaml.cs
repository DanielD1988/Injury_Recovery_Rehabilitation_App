using TestApp.views;
using Xamarin.Forms;
namespace TestApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Navigation.PushAsync(new DisplayExercises());
        }
    }
}
