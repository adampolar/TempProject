using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAURotaDAL
{
    public interface IBAUAssignmentRepository
    {
        bool Create(DateTime date, bool morning, int employeeNumber);
        List<BAUAssignment> ListFrom(DateTime dateTime);
        BAUAssignment Get(DateTime date, bool v);
    }
}
