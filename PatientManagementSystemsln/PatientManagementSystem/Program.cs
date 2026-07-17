using PatientManagementSystem.Entities;
using PatientManagementSystem.Factory;
using PatientManagementSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem
{
    internal class Program
    {
        static IPatientRepository repo = new PatientRepository();
        static void Main(string[] args)
        {
			try
			{
                DoTask();
			}
			catch (Exception ex)
			{

                Console.WriteLine(ex.Message);
			}
            finally
            {
                Console.ReadLine();
            }
        }

        private static void DoTask()
        {
            while (true)
            {
                int width = Console.WindowWidth;

                string top =  "╔══════════════════════════════════════════════════════════════════╗";
                string title ="║                     PATIENT MANAGEMENT SYSTEM                    ║";
                string div =  "╠══════════════════════════════════════════════════════════════════╣";
                string menu = "║   [1] Create    [2] View    [3] Update    [4] Delete    [5] Exit ║";
                string bottom="╚══════════════════════════════════════════════════════════════════╝";

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(top.PadLeft((width + top.Length) / 2));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(title.PadLeft((width + title.Length) / 2));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(div.PadLeft((width + div.Length) / 2));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(menu.PadLeft((width + menu.Length) / 2));
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(bottom.PadLeft((width + bottom.Length) / 2));
                Console.ResetColor();

                Console.Write("".PadLeft((width / 2) - 4));
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" Choice: ");
                Console.ResetColor();
                var input = Console.ReadLine();
                if (input == "5") break;
                ExcuteChoice(input);
            }
        }

        private static void ExcuteChoice(string choice)
        {
            switch (choice)
            {
                case "1": Create(); break;
                case "2": View();   break;
                case "3": Update(); break;
                case "4": Delete(); break;
            }

        }

        private static void Delete()
        {

            Console.Write("Enter ID to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());
            repo.DeletePatient(id);
            

        }

        private static void Update()
        {
            Console.Write("Enter Patient ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            var patient = repo.GetPatientById(id);
            if (patient == null)
            {
                Console.WriteLine("Patient not found!");
                return;
            }

            Console.Write($"New Name ({patient.Name}): ");
            patient.Name = Console.ReadLine();

            Console.Write($"New Phone ({patient.Phone}): ");
            patient.Phone = Console.ReadLine();

            Console.Write($"New Department ({patient.Dept}) - (1: Cardiology, 2: Neurology, 3: General, 4: Orthopedics): ");
            patient.Dept = (Department)Enum.Parse(typeof(Department), Console.ReadLine());

            Console.Write("New Type (1: InPatient, 2: OutPatient): ");
            patient.Type = (PatientType)int.Parse(Console.ReadLine());

            Console.Write("Medicine Cost: ");
            patient.MedicineCost = double.Parse(Console.ReadLine());

            if (patient.Type == PatientType.InPatient)
            {
                Console.Write("Total Days Admitted: ");
                patient.DaysOrVisits = double.Parse(Console.ReadLine());
                patient.VisitFee = 0;
            }
            else
            {
                Console.Write("Total Visits: ");
                patient.DaysOrVisits = double.Parse(Console.ReadLine());
                patient.DailyRate = 0;
            }

            PatientFactory factory = patient.Type == PatientType.InPatient
                ? (PatientFactory)new InPatientFactory(patient)
                : new OutPatientFactory(patient);

            factory.ProcessPatient();
            repo.UpdatePatient(patient);

            Console.WriteLine("Patient updated and recalculated successfully!");
            

        }

        private static void View()
        {
            var patientList = repo.GetPatientList();
            int width = Console.WindowWidth;

            string tableWidth = new string('═', 115);
            string divider = new string('─', 115);

            Console.WriteLine();

            // ── Title Box ──
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(("╔" + tableWidth + "╗").PadLeft((width + 117) / 2));
            Console.ForegroundColor = ConsoleColor.Green;
            string titleText ="║" + "  PATIENT INFORMATION LIST".PadLeft(71).PadRight(115) + "║";
            Console.WriteLine(titleText.PadLeft((width + titleText.Length) / 2));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(("╠" + tableWidth + "╣").PadLeft((width + 117) / 2));

            // ── Header Row ──
            Console.ForegroundColor = ConsoleColor.Yellow;
            string header = string.Format("║ {0,-4} │ {1,-17} │ {2,-13} │ {3,-11} │ {4,-12} │ {5,-11} │ {6,-13} │ {7,-11} ║","ID", "Name", "Phone", "Type", "Department", "Days/Visits", "ServiceCharge", "Total Bill");
            Console.WriteLine(header.PadLeft((width + header.Length) / 2));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(("╠" + tableWidth + "╣").PadLeft((width + 117) / 2));

            // ── Rows ──
            if (!patientList.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                string empty = "║" + " No Records Found".PadLeft(67).PadRight(115) + "║";
                Console.WriteLine(empty.PadLeft((width + empty.Length) / 2));
            }
            else
            {
                bool alternate = false;
                foreach (var p in patientList)
                {
                    string daysOrVisits = p.Type == PatientType.InPatient
                        ? $"{p.DaysOrVisits} days"
                        : $"{p.DaysOrVisits} visits";

                    // Alternate row color
                    Console.ForegroundColor = alternate ? ConsoleColor.Gray : ConsoleColor.White;

                    string row = string.Format("║ {0,-4} │ {1,-17} │ {2,-13} │ {3,-11} │ {4,-12} │ {5,-11} │ {6,-13} │ {7,-11} ║",
                        p.PatientId,
                        p.Name,
                        p.Phone,
                        p.Type,
                        p.Dept,
                        daysOrVisits,
                        p.ServiceCharge.ToString("N2"),
                        p.TotalBill.ToString("C"));

                    Console.WriteLine(row.PadLeft((width + row.Length) / 2));

                    // Divider between rows
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(("╟" + divider + "╢").PadLeft((width + 117) / 2));

                    alternate = !alternate;
                }

            }
            // ── Footer ──
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(("╚" + tableWidth + "╝").PadLeft((width + 117) / 2));

            // ── Summary ──
            Console.ForegroundColor = ConsoleColor.Green;
            int total = patientList.Count();
            string summary = $"  Total Patients: {total}  |  " +
                             $"InPatients: {patientList.Count(p => p.Type == PatientType.InPatient)}  |  " +
                             $"OutPatients: {patientList.Count(p => p.Type == PatientType.OutPatient)}";
            Console.WriteLine(summary.PadLeft((width + summary.Length) / 2));
            Console.ResetColor();
            Console.WriteLine();
        }
        private static void Create()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Patient Type (1: InPatient, 2: OutPatient): ");
            PatientType type = (PatientType)Enum.Parse(typeof(PatientType), Console.ReadLine());

            Console.Write("Department (1: Cardiology, 2: Neurology, 3: General, 4: Orthopedics): ");
            Department dept = (Department)Enum.Parse(typeof(Department), Console.ReadLine());

            Console.Write("Medicine Cost: ");
            double medicineCost = Convert.ToDouble(Console.ReadLine());

            double daysOrVisits = 0;
            if (type == PatientType.InPatient)
            {
                Console.Write("Total Days Admitted: ");
                daysOrVisits = Convert.ToDouble(Console.ReadLine());
            }
            else
            {
                Console.Write("Total Visits: ");
                daysOrVisits = Convert.ToDouble(Console.ReadLine());
            }

            Patient patient = new Patient(0, name, phone, type, dept, medicineCost, daysOrVisits);

            PatientFactory factory = type == PatientType.InPatient
                ? (PatientFactory)new InPatientFactory(patient)
                : new OutPatientFactory(patient);

            factory.ProcessPatient();
            repo.SavePatient(patient);

            Console.Write("Patient saved successfully!");
            

        }
    }
}
