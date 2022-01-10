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
        private Patient patientDetails = null;
        private List<Exercise> patientExerciselist = new List<Exercise>();
        public DisplayPatientExercisesViewModel()
        {
            fire = FirebaseMethods.GetInstance();
        }

        public async Task<List<Exercise>> getPatientExercises(string exercise1,string exercise2,string exercise3)
        {
            List<Exercise> exerciseList = await fire.GetPatientExercises(exercise1, exercise2, exercise3,false);
            return exerciseList;
        }
        public async Task<string> getExerciseVideoLink(string videoName,bool isMocked)
        {
            videoLink = await fire.GetVideosFromStorage(isMocked, videoName);
            return videoLink;
        }
        public async Task<Patient> getpatientDetails(string patientUid, bool isMocked)
        {
            patientDetails = await fire.getpatientDetails(patientUid, isMocked);
            return patientDetails;
        }
        public async Task<List<Exercise>> getPatientsExercisePlan(string patientUid, bool isMocked)
        {
            patientDetails = await getpatientDetails(patientUid, false);
            patientExerciselist = await getPatientExercises(patientDetails.Exer1, patientDetails.Exer2, patientDetails.Exer3);
            return patientExerciselist;
        }
    }
}
