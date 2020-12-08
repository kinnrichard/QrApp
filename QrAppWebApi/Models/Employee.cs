using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QrAppWebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeUsername { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeMiddleName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeePosition { get; set; }
        public string EmployeeDepartment { get; set; }
        public string EmployeeCompany { get; set; }
    }
}
