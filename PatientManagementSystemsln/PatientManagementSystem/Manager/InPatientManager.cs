using PatientManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem.Manager
{
    public class InPatientManager : IPatientManager
    {
        public double GetServiceCharge(Patient patient)
        {
            return (patient.DaysOrVisits * patient.DailyRate) * patient.ServiceChargePcnt;
        }

        public double GettotalBill(Patient patient)
        {
            double baseCost = patient.DaysOrVisits * patient.DailyRate;
            return baseCost + GetServiceCharge(patient) + patient.MedicineCost;
        }
    }
}
