using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BAURotaDAL;
using System.Collections.Generic;
using EmployeeService;
using System.Linq;
using BAURotaService;

namespace StaffBAURotaService.Test
{
    [TestClass]
    public class BAUAssignmentEligibilitySelectorTest
    {
        private List<BAUAssignment> contrivedAssignments = new List<BAUAssignment>()
        {
            //latest is thursday in the future
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 1, 0, 0, 0), Morning = false, EmployeeNumber = 8 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 1, 0, 0, 0), Morning = true, EmployeeNumber = 7 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 2, 0, 0, 0), Morning = false, EmployeeNumber = 10 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 2, 0, 0, 0), Morning = true, EmployeeNumber = 9 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 3, 0, 0, 0), Morning = false, EmployeeNumber = 6 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 3, 0, 0, 0), Morning = true, EmployeeNumber = 5 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 4, 0, 0, 0), Morning = false, EmployeeNumber = 8 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 4, 0, 0, 0), Morning = true, EmployeeNumber = 7 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 7, 0, 0, 0), Morning = false, EmployeeNumber = 6 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 7, 0, 0, 0), Morning = true, EmployeeNumber = 5 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 8, 0, 0, 0), Morning = false, EmployeeNumber = 4 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 8, 0, 0, 0), Morning = true, EmployeeNumber = 3 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 9, 0, 0, 0), Morning = false, EmployeeNumber = 2 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 9, 0, 0, 0), Morning = true, EmployeeNumber = 1 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 10, 0, 0, 0), Morning = false, EmployeeNumber = 4 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 10, 0, 0, 0), Morning = true, EmployeeNumber = 3 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 11, 0, 0, 0), Morning = false, EmployeeNumber = 2 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 11, 0, 0, 0), Morning = true, EmployeeNumber = 1 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 14, 0, 0, 0), Morning = false, EmployeeNumber = 4 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 14, 0, 0, 0), Morning = true, EmployeeNumber = 3 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 15, 0, 0, 0), Morning = false, EmployeeNumber = 2 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 15, 0, 0, 0), Morning = true, EmployeeNumber = 1 },
        };

        private List<BAUAssignment> randomAssignments = new List<BAUAssignment>()
        {
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(1, 0, 0, 0), Morning = false, EmployeeNumber = 10 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(1, 0, 0, 0), Morning = true, EmployeeNumber = 6 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(2, 0, 0, 0), Morning = false, EmployeeNumber = 1 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(2, 0, 0, 0), Morning = true, EmployeeNumber = 2 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(3, 0, 0, 0), Morning = false, EmployeeNumber = 6 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(3, 0, 0, 0), Morning = true, EmployeeNumber = 5 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(4, 0, 0, 0), Morning = false, EmployeeNumber = 4 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(4, 0, 0, 0), Morning = true, EmployeeNumber = 2 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(7, 0, 0, 0), Morning = false, EmployeeNumber = 3 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(7, 0, 0, 0), Morning = true, EmployeeNumber = 9 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(8, 0, 0, 0), Morning = false, EmployeeNumber = 7 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(8, 0, 0, 0), Morning = true, EmployeeNumber = 5 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(9, 0, 0, 0), Morning = false, EmployeeNumber = 3 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(9, 0, 0, 0), Morning = true, EmployeeNumber = 4 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(10, 0, 0, 0), Morning = false, EmployeeNumber = 5 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(10, 0, 0, 0), Morning = true, EmployeeNumber = 6 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(11, 0, 0, 0), Morning = false, EmployeeNumber = 8 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(11, 0, 0, 0), Morning = true, EmployeeNumber = 1 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(14, 0, 0, 0), Morning = false, EmployeeNumber = 5 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(14, 0, 0, 0), Morning = true, EmployeeNumber = 2 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(15, 0, 0, 0), Morning = false, EmployeeNumber = 7 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(15, 0, 0, 0), Morning = true, EmployeeNumber = 1 },
        };


        private List<int> employees = new List<int>()
        {
            1,2,3,4,5,6,7,8,9,10
        };

        [TestMethod]
        public void TestIfOnly6SessionsLeftInTwoWeekPeriodAndContrivedSituation()
        {
            //given 
            var tstAss = contrivedAssignments.Skip(4).Take(14).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 2, 0, 0, 0), true, 
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 9, 10 }, elEmps);

        }

        [TestMethod]
        public void TestIfOnly5SessionsLeftInTwoWeekPeriodAndContrivedSituation()
        {
            //given 
            var tstAss = contrivedAssignments.Skip(3).Take(15).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 2, 0, 0, 0), false,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 10 }, elEmps);

        }

        [TestMethod]
        public void TestIfOnly4SessionsLeftInTwoWeekPeriodAndContrivedSituation()
        {
            //given 
            var tstAss = contrivedAssignments.Skip(2).Take(20).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 1, 0, 0, 0), false,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 7, 8 }, elEmps);

        }

        [TestMethod]
        public void TestIfOnly3SessionsLeftInTwoWeekPeriodAndContrivedSituation()
        {
            //given 
            var tstAss = contrivedAssignments.Skip(1).Take(20).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5 + 1, 0, 0, 0), false,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 8}, elEmps);

        }

        [TestMethod]
        public void TestIfOnly2SessionsLeftInTwoWeekPeriodAndContrivedSituation()
        {
            //given 
            var tstAss = contrivedAssignments.Take(20).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan((int)DateTime.Now.DayOfWeek - 5, 0, 0, 0), false,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 9, 10 }, elEmps);

        }

        [TestMethod]
        public void TestNoEmployeesOnConsecutiveDays()
        {
            //given
            var tstAss = new List<BAUAssignment>();
            tstAss.Add(new BAUAssignment() { Date = DateTime.Now.Date - new TimeSpan(1, 0, 0, 0), Morning = true, EmployeeNumber = 7 });
            tstAss.Add(new BAUAssignment() { Date = DateTime.Now.Date - new TimeSpan(1, 0, 0, 0), Morning = false, EmployeeNumber = 8 });
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date, true,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 1,2,3,4,5,6,9,10 }, elEmps);
        }

        [TestMethod]
        public void TestNoEmployeesTwiceOnOneDay()
        {
            //given
            var tstAss = new List<BAUAssignment>();
            tstAss.Add(new BAUAssignment() { Date = DateTime.Now.Date, Morning = true, EmployeeNumber = 7 });
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date, false,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 4, 5, 6, 8, 9, 10 }, elEmps);
        }

    }
}
