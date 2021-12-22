using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.ViewModels
{
    class RPatientViewModel
    {

        public string removeTimeFromDate(string dateTime)
        {
            int spaceFound = dateTime.IndexOf(" ");
            string date = dateTime.Substring(0, spaceFound);
            return date;
        }
    }
}
