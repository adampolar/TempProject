using System.Collections.Generic;

namespace EmployeeService
{
    public interface IEmployeeRepository
    {
        Employee Get(int employeeNumber);
        List<Employee> List();
    }
}