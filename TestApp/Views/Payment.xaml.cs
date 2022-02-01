using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Payment : ContentPage
    {
        PaymentViewModel pay;
        public Payment()
        {
            pay = new PaymentViewModel();
            InitializeComponent();
        }
        public void makePayment(Object Sender, EventArgs args)
        {
            pay.makePayment("", 1, 1, "", "", "", 1);
        }
    }
}