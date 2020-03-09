using WorkBench.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkBench.Models;

namespace WorkBench.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly optisolapiContext context;

        public EmployeeRepository(optisolapiContext context)
        {
            this.context = context;
        }


        public Employees DeleteEmployee(int Id)
        {
            Employees employee = context.Employees.Find(Id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }


        public IEnumerable<Employees> GetAllEmployees()
        {
            return context.Employees;
        }


        public Employees GetEmployee(int Id)
        {
            return context.Employees.Find(Id);
        }

        public Employees PostEmployee(Employees employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employees PutEmployee(Employees employeechanges)
        {

            var employee = context.Employees.Attach(employeechanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            /* context.Entry(employeechanges).State = Microsoft.EntityFrameworkCore.EntityState.Modified;*/
            context.SaveChanges();
            return employeechanges;
            /*
            Employee employee = context.Employees.FirstOrDefault(e => e.Id == employeechanges.Id);
            if (employee != null)
            {
                employee.Name = employeechanges.Name;
                employee.Email = employeechanges.Email;
                employee.Department = employeechanges.Department;
                context.SaveChanges();                
            }
            return employee;
            */
        }
    }
}
