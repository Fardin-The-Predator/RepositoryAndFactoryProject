using PatientManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem.Manager
{
    public interface IPatientManager
    {
        double GetServiceCharge(Patient patient);
        double GettotalBill(Patient patient);

    }
}
