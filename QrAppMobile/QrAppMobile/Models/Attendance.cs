using System;
using System.Collections.Generic;
using System.Text;

namespace QrAppMobile.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string Location { get; set; }
    }
}
