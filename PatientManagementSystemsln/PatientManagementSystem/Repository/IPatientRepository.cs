using PatientManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem.Repository
{
    public interface IPatientRepository
    {
        void SavePatient(Patient patient);
        IEnumerable<Patient> GetPatientList();
        Patient GetPatientById(int id);
        void UpdatePatient(Patient patient);
        void DeletePatient(int id);
    }
}
