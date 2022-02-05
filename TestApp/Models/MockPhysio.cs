using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Models
{
    public class MockPhysio
    {
        //extra fields for mock database
        public string PhysioName { get; set; }
        public string Email { get; set; }
        public string IdNumber { get; set; }
        public List<string> patientList = new List<string>();
        public string physioUid = "";
        public string PatientUid { get; set; }
        public string Membership { get; set; }
    }
}
