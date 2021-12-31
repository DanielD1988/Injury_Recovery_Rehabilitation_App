using System;
using System.Collections.Generic;
using TestApp.models;
using TestApp.Models;

namespace TestApp.Services
{
    /// <summary>
    /// This class mocks data that represents the data from the real database
    /// </summary>
    public class MockDatabase
    {
        /// <summary>
        /// This method returns all exercise plans of a JSON list called exercisePlans
        /// </summary>
        /// <returns></returns>
        public List<ExercisePlan> GetAllMockExercises()
        {
            List<ExercisePlan> plans = new List<ExercisePlan>()
            {
                new ExercisePlan(){
                    exerciseName = "Knee pain",
                    Category = "Lower Extremity",
                    exerciseDescription = "Knee pain can affect people of all ages and can be a result of an injury caused by torn cartilage or arthritis",
                    Exercise1 = "Heel and calf stretch",
                    Exercise2 = "Hamstring stretch"
                }, 
                new ExercisePlan(){
                    exerciseName = "Bells palsy",
                    Category = "Face",
                    exerciseDescription = "The symptoms of Bell's palsy include sudden weakness in your facial muscles",
                },
            };
            return plans;
        }
        /// <summary>
        /// This method returns a single exercise from a list of exercises using an exercise key
        /// </summary>
        /// <param name="exerciseKey"></param>
        /// <returns></returns>
        public ExercisePlan GetMockExercise(String exerciseKey)
        {
            List<ExercisePlan> plans = GetAllMockExercises();

            foreach (ExercisePlan plan in plans)
            {
                if(plan.exerciseName == exerciseKey)
                {
                    return plan;
                }
            }
            return null;
        }
        /// <summary>
        /// This method inserts a new patient entry into the patient database using the patientUid as the key
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="injuryType"></param>
        /// <param name="injuryOccurred"></param>
        /// <param name="age"></param>
        /// <param name="injurySeverity"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="exerPlan"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool AddMockPatient(string patientUid, string name, string gender, string injuryType, string injuryOccurred, int age, int injurySeverity, DateTime startDate, DateTime endDate, string exerPlan, string email)
        {
          Patient newPatient = null;
          newPatient = new Patient() { 
              PatientName = name};
          if(newPatient.PatientName != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// This function adds the patient unique identifier to the physios patient list
        /// </summary>
        /// <param name="PhysioUid"></param>
        /// <param name="PatientUID"></param>
        /// <returns></returns>
        public bool AddMockPatientUIDToPatientList(string PhysioUid, string PatientUID)
        {
            List<Physiotherapist> physioList = new List<Physiotherapist>() 
            { 
                new Physiotherapist{PhysioName = "john Denver",Email = "johnDenver@gmail.com",physioUid = "adcd4321"}
            };

            foreach (Physiotherapist physio in physioList)
            {
                if(PhysioUid == physio.physioUid)
                {
                    physio.patientList.Add(PatientUID);
                    return true;
                }
            }
            return false;
        }
    }
}
