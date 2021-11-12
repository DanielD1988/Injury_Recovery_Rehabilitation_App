using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class TestViewModel : BindableObject
    {
        public TestViewModel()
        {
            IncreaseCount = new Command(OnIncrease);
        }

        int count = 0;
        string countDisplay = "Click Here";

        public ICommand IncreaseCount { get; }

        public string CountDisplay
        {
            get => countDisplay;
            set
            {
                if (value == countDisplay)
                {
                    return;
                }
                countDisplay = value;
                OnPropertyChanged();
            }
        }
        void OnIncrease()
        {
            count++;
            CountDisplay = $"You clicked {count} times";
        }
    }
}
