using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.services;

namespace TestApp.ViewModels
{
    class DisplayPatientExercisesViewModel
    {
        private FirebaseMethods fire;
        private string videoLink = "";
        public DisplayPatientExercisesViewModel()
        {
            fire = FirebaseMethods.GetInstance();
        }

        public List<Exercise> getPatientExercises(string exercise1,string exercise2,string exercise3)
        {
            List<Exercise> exerciseList = fire.GetPatientExercises(false).Result;
            return exerciseList;
        }
        public async Task<string> getExerciseVideoLink(string videoName,bool isMocked)
        {
            videoLink = await fire.GetVideosFromStorage(isMocked, videoName);
            return videoLink;
        }
    }
}
