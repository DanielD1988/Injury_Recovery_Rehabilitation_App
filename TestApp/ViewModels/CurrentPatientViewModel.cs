using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.services;

namespace TestApp.ViewModels
{
    class CurrentPatientViewModel
    {
        private FirebaseMethods fire;
        private Patient patientDetails = null;
        public CurrentPatientViewModel()
        {
            fire = FirebaseMethods.GetInstance();
        }
        public async Task<Patient> getpatientDetails(string patientUid, bool isMocked)
        {
            patientDetails = await fire.getpatientDetails(patientUid, isMocked);
            return patientDetails;
        }
    }
}
