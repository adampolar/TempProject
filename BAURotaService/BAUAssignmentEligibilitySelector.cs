using System;
using System.Collections.Generic;
using System.Linq;
using BAURotaDAL;

namespace BAURotaService
{
    public class BAUAssignmentEligibilitySelector : IBAUAssignmentEligibilitySelector
    {
        public List<int> ListEligibleEmployees(DateTime date, bool morning, List<BAUAssignment> lastTwoWeeksAssignmentsOrMore, List<int> allEmployees)
        {
            IEnumerable<int> eligibleEmployees = allEmployees;
            /*
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
            }*/


            var lastSunday = date.AddDays(-(int)date.DayOfWeek - 7);
            var assignmentsInLastTwoCalendarWeeks = lastTwoWeeksAssignmentsOrMore.Where(a => a.Date > lastSunday);
            var employeesWithAssignmentCounts = assignmentsInLastTwoCalendarWeeks
                .GroupBy(a => a.EmployeeNumber)
                .Select(g => new { EmployeeNumber = g.Key, Count = g.Count() });

            //add in employees with no assignments (probably a better way of doing this
            foreach (var empNo in allEmployees)
            {
                if (employeesWithAssignmentCounts.Where(c => c.EmployeeNumber == empNo).Count() == 0)
                {
                    var list = employeesWithAssignmentCounts.ToList();
                    list.Add( new { EmployeeNumber = empNo, Count = 0 });
                    employeesWithAssignmentCounts = list;
                }
            }

            var employeesWhoHaveHadLessThan2Assignments = employeesWithAssignmentCounts
                .Where(a => a.Count < 2);


            //i think theres a special case around if you have 2 left with 0 assignments 
            // and 2 left with 1 assignments then pick the 2 without assignments be
            // stems from worse case scenario of ab,cd,ab,cd,ef,gh,ef,
            // then you need to pick ij

            if (assignmentsInLastTwoCalendarWeeks.Count() >= 14)
            {
                if (employeesWhoHaveHadLessThan2Assignments.Where(a => a.Count == 0).Count() == 2
                    && employeesWhoHaveHadLessThan2Assignments.Where(a => a.Count == 1).Count() == 2)
                {
                    return employeesWhoHaveHadLessThan2Assignments
                        .Where(a => a.Count == 0)
                        .Select(a => a.EmployeeNumber)
                        .ToList();
                }

                //also for that afternoon
                if (employeesWhoHaveHadLessThan2Assignments.Where(a => a.Count == 0).Count() == 1
                    && employeesWhoHaveHadLessThan2Assignments.Where(a => a.Count == 1).Count() == 3)
                {
                    return employeesWhoHaveHadLessThan2Assignments
                        .Where(a => a.Count == 0)
                        .Select(a => a.EmployeeNumber)
                        .ToList(); //boring wheel of fortune, only one employee
                }
            }

            eligibleEmployees = employeesWhoHaveHadLessThan2Assignments.Select(a => a.EmployeeNumber);



            //select developers who are not in the other half of the day

            eligibleEmployees = eligibleEmployees.Where(x =>
            lastTwoWeeksAssignmentsOrMore.Where(
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
            lastTwoWeeksAssignmentsOrMore.Where(
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