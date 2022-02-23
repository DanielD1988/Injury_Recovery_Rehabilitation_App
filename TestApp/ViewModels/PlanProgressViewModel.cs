using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.services;

namespace TestApp.ViewModels
{
    class PlanProgressViewModel
    {
        private FirebaseMethods fire;
        public PlanProgressViewModel()
        {
            fire = FirebaseMethods.GetInstance();
        }
        public async Task<Dictionary<string, bool>> getPatientProgress(string patientUid)
        {
            return await fire.getPatientProgress(patientUid, false);
        }
        public async Task <List<PatientList>> getPatientNameAndPatientUserId(string physioId)
        {
            return await fire.GetNamesAndPatientUids(physioId, false); 
        }
    }
}
