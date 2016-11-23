using System;
using System.Collections.Generic;
using System.Linq;
using BAURotaDAL;

namespace BAURotaService
{
    public class BAUAssignmentEligibilitySelector : IBAUAssignmentEligibilitySelector
    {
        public List<int> ListEligibleEmployees(DateTime date, bool morning, List<BAUAssignment> LastTwoWeeksAssignments, List<int> allEmployees)
        {
            IEnumerable<int> eligibleEmployees = allEmployees;

            //ensure any developers who haven't done bau in 2 weeks are selected
            //has to be thought of going forward
            //for example if there are 6 people that havent done any shifts in the last (2 weeks - 3 days) then only they can be selected
            //in a given week you have to have at least 4 developers completed a shift due to other rules below
            //so check from 2 weeks - 3 days and go to 2 weeks bcause we can always fit the 6 in the last two weeks
            // note current rules assume 10 devs
            if (LastTwoWeeksAssignments.Count() >= 14)
            {
                for (int i = 6; i > 0; i--)
                {
                    IEnumerable<int> lastSet = allEmployees.Except(
                        GetLastXAssignments(LastTwoWeeksAssignments, 20 - i).Select(a => a.EmployeeNumber)
                    );

                    if (lastSet.Count() >= i)
                    {
                        eligibleEmployees = lastSet;
                        break;
                    }
                }
            }
            
            //select developers who are not in the other half of the day

            eligibleEmployees = eligibleEmployees.Where(x =>
            LastTwoWeeksAssignments.Where(
                a =>
                (
                a.Date == date &&
                a.Morning == !morning &&
                a.EmployeeNumber == x
                )
                ).Count() == 0
                );


            // who are not on the day previous

            eligibleEmployees = eligibleEmployees.Where(x =>
            LastTwoWeeksAssignments.Where(
                a =>
                (
                a.Date == date - new TimeSpan(1, 0, 0, 0) &&
                a.EmployeeNumber == x
                )
                ).Count() == 0
                );


            return eligibleEmployees.ToList();
        }

        private IEnumerable<BAUAssignment> GetLastXAssignments(List<BAUAssignment> bauAssignments, int howMany)
        {
            bauAssignments.Sort(new BAUOrderer());
            return bauAssignments.Take(howMany);;
        }

        private class BAUOrderer : IComparer<BAUAssignment>
        {
            public int Compare(BAUAssignment x, BAUAssignment y)
            {
                if (x.Date.Date < y.Date.Date)
                    return 1;
                if (x.Date.Date > y.Date.Date)
                    return -1;
                if (x.Date.Date == y.Date.Date)
                {
                    return x.Morning ? 1 : -1; //should never have the same date and session
                }

                return 0;

            }
        }
    }
}