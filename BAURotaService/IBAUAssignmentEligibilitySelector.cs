using BAURotaDAL;
using System;
using System.Collections.Generic;

namespace BAURotaService
{
    public interface IBAUAssignmentEligibilitySelector
    {

        List<int> ListEligibleEmployees(DateTime date, bool morning, List<BAUAssignment> LastTwoWeeksAssignments, List<int> allEmployees);

    }
}