using PatientManagementSystem.Entities;
using PatientManagementSystem.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem.Factory
{
    public abstract class PatientFactory
    {
        protected Patient _patient;

        protected PatientFactory(Patient patient)
        {
            _patient = patient;
        }
        public abstract IPatientManager Create();

        public Patient ProcessPatient()
        {
            IPatientManager manager= Create();
            _patient.ServiceCharge = manager.GetServiceCharge(_patient);
            _patient.TotalBill = manager.GettotalBill(_patient);
            return _patient;
        }
    }
}
