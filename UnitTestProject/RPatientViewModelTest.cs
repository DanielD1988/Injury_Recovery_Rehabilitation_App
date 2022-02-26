﻿using System.Collections.Generic;
using TestApp.models;
using TestApp.ViewModels;
using Xunit;

namespace UnitTestProject
{
    public class RPatientViewModelTest
    {
        RegisterPatientViewModel patientVm = new RegisterPatientViewModel();
        /// <summary>
        /// This method checks if the SendPatientEmail is called can not open an xamarin essentails email from a test 
        /// so test does fail
        /// </summary>
        [Fact]
        public async void SendPatientEmailTest()
        {
            List<string> emailList = new List<string>();
            emailList.Add("john@gmail.com");
            bool result = await patientVm.SendPatientEmail(emailList, "asdwr252sg", "body of email", "this is the subject");
            bool expected = false;
            Assert.Equal(expected, result);
        }
        [Fact]
        public async void setUpPatientAccountTest()
        {
            Dictionary<string, bool> planDates = new Dictionary<string, bool>();
            planDates.Add("12-02-2022", false);
            ExercisePlan plan = new ExercisePlan()
            {
                Category = "Lower Extremity",
                Exercise1 = "Heel and calf stretch",
                Exercise2 = "Hamstring stretch",
                Exercise3 = "Prisoner Squats",
                exerciseDescription = "",
                exerciseInfo = "",
                exerciseListKey = "",
                exerciseName = "Knee Pain",
                Image = "",
                ImageBase64 = ""
            };
            bool result = await patientVm.setUpPatientAccount("John Dooly", "Male", "john@gmail.com", "sprain", "dancing", 45, 4, planDates, plan, "1acdf4532", null, null, 1, 1, 1, 2, 2, 2, true);
            Assert.True(result);
        }
    }
}
