using System;
using System.Collections.Generic;

namespace WorkBench.Models
{
    public partial class Employees
    {
        public int Id { get; set; }
        public string EmpName { get; set; }
        public string EmailId { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
