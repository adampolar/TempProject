using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeService
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> employees = new List<Employee>()
        {
            new Employee { StaffNumber = 1, FirstName = "Audra", SecondName = "Arms", Image = null },
            new Employee { StaffNumber = 2, FirstName = "Benton", SecondName = "Bowker", Image = null },
            new Employee { StaffNumber = 3, FirstName = "Chad", SecondName = "Caddy", Image = null },
            new Employee { StaffNumber = 4, FirstName = "Dorothy", SecondName = "Daughtry", Image = null },
            new Employee { StaffNumber = 5, FirstName = "Edyth", SecondName = "Emery", Image = null },
            new Employee { StaffNumber = 6, FirstName = "Freddie", SecondName = "Frew", Image = null },
            new Employee { StaffNumber = 7, FirstName = "Gilbert", SecondName = "Goza", Image = null },
            new Employee { StaffNumber = 8, FirstName = "Hulda", SecondName = "Hogsett", Image = null },
            new Employee { StaffNumber = 9, FirstName = "Isabelle", SecondName = "Iglesias", Image = null },
            new Employee { StaffNumber = 10, FirstName = "Jamar", SecondName = "Janssen", Image = null }
        };

        public Employee Get(int employeeNumber)
        {
            return employees.Where(e => e.StaffNumber == employeeNumber).FirstOrDefault();
        }

        public List<Employee> List()
        {
            return employees;
        }
    }
}