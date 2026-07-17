using PatientManagementSystem.Entities;
using PatientManagementSystem.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem.Factory
{
    public class OutPatientFactory :PatientFactory
    {
        public OutPatientFactory(Patient patient) : base(patient) { }

        public override IPatientManager Create()
        {
            return new OutPatientManager();
        }
    }
}
