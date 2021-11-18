using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TestApp.models;
using TestApp.services;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage1 : ContentPage
    {
        private FirebaseMethods fire;
        public ListViewPage1()
        {
            fire = new FirebaseMethods();
            InitializeComponent();
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MyListView.ItemsSource = null;
            var exercises = await fire.GetAllExercises();
            MyListView.ItemsSource = exercises;
        }

    }
}
