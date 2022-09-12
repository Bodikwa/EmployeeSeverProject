

namespace EmployeeSever.Models
{
    public class Employee
    {
        //Remember the modle must be the same as the properties in tthe datra base
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeEmail { get; set; }
    }
}
