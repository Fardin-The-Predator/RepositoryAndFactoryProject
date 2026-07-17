using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem.Entities
{
    public class Patient
    {
        public Patient()
        {
            
        }
        public Patient(int patientId, string name,string phone, PatientType type,Department dept, double medicineCost, double daysOrVisits)
        {
           PatientId=patientId;
            Name = name;
            Phone = phone;
            Type = type;
            Dept = dept;
            MedicineCost = medicineCost;
            DaysOrVisits = daysOrVisits;
            DailyRate = 2000;    // default daily rate for InPatient
            VisitFee = 500;     // default visit fee for OutPatient
            ServiceChargePcnt = 0.10; // 10% service charge

        }
        public int PatientId { get; set; } 
        public string Name { get; set; }
        public string Phone { get; set; }
        public PatientType Type { get; set; }
        public Department Dept { get; set; }
        public double MedicineCost { get; set; }
        public double DaysOrVisits { get; set; }
        public double DailyRate { get; set; }
        public double VisitFee { get; set; }
        public double ServiceChargePcnt { get; set; }
        public double ServiceCharge {  get; set; }
        public double  TotalBill { get; set; }

    }
}
