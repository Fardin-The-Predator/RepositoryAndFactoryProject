using PatientManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private List<Patient> db = new List<Patient>();


        

        public PatientRepository()
        {
            db = new List<Patient>()
    {
        new Patient()
        {
            PatientId         = 1,
            Name              = "Ahmed Hassan",
            Phone             = "01754568785",
            Type              = PatientType.InPatient,
            Dept              = Department.Cardiology,
            MedicineCost      = 5000,
            DaysOrVisits      = 7,
            DailyRate         = 2000,
            VisitFee          = 0,
            ServiceChargePcnt = 0.10,
            ServiceCharge     = 1400,       // 7 x 2000 x 10%
            TotalBill         = 20400        // (7x2000) + 1400 + 5000
        },

        new Patient()
        {
            PatientId         = 2,
            Name              = "Fatima Begum",
            Phone             = "01872635352",
            Type              = PatientType.OutPatient,
            Dept              = Department.General,
            MedicineCost      = 1500,
            DaysOrVisits      = 3,
            DailyRate         = 0,
            VisitFee          = 500,
            ServiceChargePcnt = 0.10,
            ServiceCharge     = 0,           // No service charge for OutPatient
            TotalBill         = 3000         // (3x500) + 1500
        },

        new Patient()
        {
            PatientId         = 3,
            Name              = "Rahim Uddin",
            Phone             = "01338237372",
            Type              = PatientType.InPatient,
            Dept              = Department.Neurology,
            MedicineCost      = 8000,
            DaysOrVisits      = 10,
            DailyRate         = 2000,
            VisitFee          = 0,
            ServiceChargePcnt = 0.10,
            ServiceCharge     = 2000,        // 10 x 2000 x 10%
            TotalBill         = 30000        // (10x2000) + 2000 + 8000
        },

        new Patient()
        {
            PatientId         = 4,
            Name              = "Nadia Islam",
            Phone             = "01628197326",
            Type              = PatientType.OutPatient,
            Dept              = Department.Orthopedics,
            MedicineCost      = 2000,
            DaysOrVisits      = 5,
            DailyRate         = 0,
            VisitFee          = 500,
            ServiceChargePcnt = 0.10,
            ServiceCharge     = 0,           // No service charge for OutPatient
            TotalBill         = 4500         // (5x500) + 2000
        }
    };
        }

        public IEnumerable<Patient> GetPatientList() => db;

        public Patient GetPatientById(int id) =>
            db.FirstOrDefault(p => p.PatientId == id);




        public void SavePatient(Patient patient)
        {
            patient.PatientId = db.Count > 0 ? db.Max(p => p.PatientId) + 1 : 1;
            db.Add(patient);
        }
        public void UpdatePatient(Patient updatedPatient)
        {
            Patient existing = GetPatientById(updatedPatient.PatientId);
            if (existing != null)
            {
                existing.Name = updatedPatient.Name;
                existing.Phone = updatedPatient.Phone;
                existing.Type = updatedPatient.Type;
                existing.Dept = updatedPatient.Dept;
                existing.MedicineCost = updatedPatient.MedicineCost;
                existing.DaysOrVisits = updatedPatient.DaysOrVisits;
                existing.DailyRate = updatedPatient.DailyRate;
                existing.VisitFee = updatedPatient.VisitFee;
                existing.ServiceCharge = updatedPatient.ServiceCharge;
                existing.TotalBill = updatedPatient.TotalBill;
            }
        }

        public void DeletePatient(int id)
        {
            Patient patient = GetPatientById(id);
            if (patient != null)
                db.Remove(patient);
        }

       

       

       
    }
}
