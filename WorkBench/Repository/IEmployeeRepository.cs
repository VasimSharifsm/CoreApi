using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkBench.Models;

namespace WorkBench.Repository
{
    public interface IEmployeeRepository
    {
        Employees GetEmployee(int Id);
        IEnumerable<Employees> GetAllEmployees();
        Employees PostEmployee(Employees employee);
        Employees PutEmployee(Employees employeechanges);
        Employees DeleteEmployee(int Id);
    }
}
