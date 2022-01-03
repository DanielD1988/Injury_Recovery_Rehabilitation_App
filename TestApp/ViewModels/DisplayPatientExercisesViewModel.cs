using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TestApp.Models;
using TestApp.services;

namespace TestApp.ViewModels
{
    class DisplayPatientExercisesViewModel
    {
        private FirebaseMethods fire;
        public DisplayPatientExercisesViewModel()
        {
            fire = FirebaseMethods.GetInstance();
        }

        public List<Exercise> getPatientExercises(string exercise1,string exercise2,string exercise3)
        {
            List<Exercise> exerciseList = fire.GetPatientExercises(false).Result;
            return exerciseList;
        }

    }
}
