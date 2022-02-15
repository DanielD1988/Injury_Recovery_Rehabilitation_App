using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayProgress : ContentPage
    {
        PlanProgressViewModel plan = new PlanProgressViewModel();
        Dictionary<string, bool> progressPlan;
        int pages = 0;
        string stringDate = "";
        public DisplayProgress(string uid, Dictionary<string, bool> progressPlan,int pages)
        {
            InitializeComponent();
            this.progressPlan = progressPlan;
            this.pages = pages;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            populateCalander(progressPlan);
        }
        public void populateCalander(Dictionary<string, bool> progressPlan)
        {
            CultureInfo objcul = new CultureInfo("en-GB");
            calendar.SpecialDates = new List<XamForms.Controls.SpecialDate>();
            int i = 0;
            while (i < progressPlan.Count)
            {
                stringDate = progressPlan.Keys.ElementAt(i);
                stringDate = stringDate.Replace("-", "/");
                
                DateTime currentDate = DateTime.ParseExact(stringDate, "dd/MM/yyyy", objcul); ;
                if (currentDate.Date == DateTime.Today)
                {
                    calendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(currentDate) { BackgroundColor = Color.SteelBlue, TextColor = Color.Black, BorderColor = Color.White, BorderWidth = 0, Selectable = false });
                }
                else
                {
                    if (progressPlan.Values.ElementAt(i) == true)
                    {

                        calendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(currentDate) { BackgroundColor = Color.Green, TextColor = Color.Black, BorderColor = Color.White, BorderWidth = 0, Selectable = false });
                    }
                    else
                    {

                        calendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(currentDate) { BackgroundColor = Color.Red, TextColor = Color.Black, BorderColor = Color.White, BorderWidth = 0, Selectable = false });
                    }
                }
                i++;
            }
        }
        async void menu(object sender, EventArgs e)
        {
            if(pages == 3)
            {
                await Navigation.PopToRootAsync();
            }
            else
            {
                await Navigation.PopModalAsync();
            }
        }
    }
}