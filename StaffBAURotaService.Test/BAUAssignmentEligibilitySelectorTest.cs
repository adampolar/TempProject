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
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(1, 0, 0, 0), Morning = false, EmployeeNumber = 10 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(1, 0, 0, 0), Morning = true, EmployeeNumber = 9 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(2, 0, 0, 0), Morning = false, EmployeeNumber = 8 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(2, 0, 0, 0), Morning = true, EmployeeNumber = 7 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(3, 0, 0, 0), Morning = false, EmployeeNumber = 6 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(3, 0, 0, 0), Morning = true, EmployeeNumber = 5 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(4, 0, 0, 0), Morning = false, EmployeeNumber = 4 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(4, 0, 0, 0), Morning = true, EmployeeNumber = 3 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(7, 0, 0, 0), Morning = false, EmployeeNumber = 2 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(7, 0, 0, 0), Morning = true, EmployeeNumber = 1 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(8, 0, 0, 0), Morning = false, EmployeeNumber = 4 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(8, 0, 0, 0), Morning = true, EmployeeNumber = 3 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(9, 0, 0, 0), Morning = false, EmployeeNumber = 2 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(9, 0, 0, 0), Morning = true, EmployeeNumber = 1 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(10, 0, 0, 0), Morning = false, EmployeeNumber = 4 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(10, 0, 0, 0), Morning = true, EmployeeNumber = 3 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(11, 0, 0, 0), Morning = false, EmployeeNumber = 2 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(11, 0, 0, 0), Morning = true, EmployeeNumber = 1 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(14, 0, 0, 0), Morning = false, EmployeeNumber = 4 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(14, 0, 0, 0), Morning = true, EmployeeNumber = 3 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(15, 0, 0, 0), Morning = false, EmployeeNumber = 2 },
            new BAUAssignment() { Date=DateTime.Now.Date - new TimeSpan(15, 0, 0, 0), Morning = true, EmployeeNumber = 1 },
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
        public void TestIfOnly6SessionsLeftInTwoWeekPeriodAnd6PeopleAreLeftThenReturn6()
        {
            //given 
            var tstAss = contrivedAssignments.Skip(6).Take(20).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan(3, 0, 0, 0), true, 
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 5, 6, 7, 8, 9, 10 }, elEmps);

        }

        [TestMethod]
        public void TestIfOnly5SessionsLeftInTwoWeekPeriodAnd5PeopleAreLeftThenReturn6()
        {
            //given 
            var tstAss = contrivedAssignments.Skip(5).Take(20).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan(3, 0, 0, 0), false,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 6, 7, 8, 9, 10 }, elEmps);

        }

        [TestMethod]
        public void TestIfOnly4SessionsLeftInTwoWeekPeriodAnd4PeopleAreLeftThenReturn6()
        {
            //given 
            var tstAss = contrivedAssignments.Skip(4).Take(20).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan(2, 0, 0, 0), true,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 7, 8, 9, 10 }, elEmps);

        }

        [TestMethod]
        public void TestIfOnly3SessionsLeftInTwoWeekPeriodAnd3PeopleAreLeftThenReturn6()
        {
            //given 
            var tstAss = contrivedAssignments.Skip(3).Take(20).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan(2, 0, 0, 0), false,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 8, 9, 10 }, elEmps);

        }

        [TestMethod]
        public void TestIfOnly2SessionsLeftInTwoWeekPeriodAnd2PeopleAreLeftThenReturn6()
        {
            //given 
            var tstAss = contrivedAssignments.Skip(2).Take(20).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan(1, 0, 0, 0), true,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 9, 10 }, elEmps);

        }

        [TestMethod]
        public void TestIfOnly1SessionsLeftInTwoWeekPeriodAnd1PeopleAreLeftThenReturn6()
        {
            //given
            var tstAss = contrivedAssignments.Skip(1).Take(20).ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date - new TimeSpan(1, 0, 0, 0), false,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 10 }, elEmps);

        }

        [TestMethod]
        public void TestNoEmployeesOnConsecutiveDays()
        {
            //given
            var tstAss = randomAssignments.ToList();
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date, true,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 1,2,3,4,5,7,8,9 }, elEmps);
        }

        [TestMethod]
        public void TestNoEmployeesTwiceOnOneDay()
        {
            //given
            var tstAss = new List<BAUAssignment>();
            tstAss.AddRange(randomAssignments);
            tstAss.Add(new BAUAssignment() { Date = DateTime.Now.Date, Morning = true, EmployeeNumber = 7 });
            //when
            var elEmps = new BAUAssignmentEligibilitySelector().ListEligibleEmployees(
                DateTime.Now.Date, false,
                tstAss, employees);
            //then
            CollectionAssert.AreEquivalent(new List<int>() { 1, 2, 3, 4, 5, 8, 9 }, elEmps);
        }

    }
}
