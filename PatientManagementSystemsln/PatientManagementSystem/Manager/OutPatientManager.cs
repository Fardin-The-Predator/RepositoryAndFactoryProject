using PatientManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem.Manager
{
    public class OutPatientManager : IPatientManager
    {
        public double GetServiceCharge(Patient patient)
        {
            return 0;
        }

        public double GettotalBill(Patient patient)
        {
            return (patient.DaysOrVisits * patient.VisitFee) + patient.MedicineCost;
        }
    }
}
