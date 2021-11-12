using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TestApp.models;
using TestApp.services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage1 : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public ListViewPage1()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "hi",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        protected async override void OnAppearing()
        {
            FirebaseMethods fire = new FirebaseMethods();
            base.OnAppearing();

            List<Exercise> exercises = await fire.GetAllExercises();

            foreach (Exercise exercise in exercises)
            {
                Console.Write(exercise.exerciseName);
                Console.Write(exercise.exerciseDescription);
            }
        }
    }
}
