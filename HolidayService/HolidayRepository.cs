using System;
using System.Collections.Generic;

namespace HolidayService
{
    internal class HolidayRepository : IHolidayRepository
    {

        Dictionary<Tuple<int, int>, List<int>> employeeHolidaysByDate = new Dictionary<Tuple<int, int>, List<int>>()
        {
            { Tuple.Create(1,12), new List<int>() },
            { Tuple.Create(2,12), new List<int>() },
            { Tuple.Create(3,12), new List<int>() },
            { Tuple.Create(4,12), new List<int>() },
            { Tuple.Create(5,12), new List<int>() },
            { Tuple.Create(6,12), new List<int>() },
            { Tuple.Create(7,12), new List<int>() },
            { Tuple.Create(8,12), new List<int>() },
            { Tuple.Create(9,12), new List<int>() },
            { Tuple.Create(10,12), new List<int>() },
            { Tuple.Create(11,12), new List<int>() },
            { Tuple.Create(12,12), new List<int>() },
            { Tuple.Create(13,12), new List<int>() },
            { Tuple.Create(14,12), new List<int>() },
            { Tuple.Create(15,12), new List<int>() },
            { Tuple.Create(16,12), new List<int>() },
            { Tuple.Create(17,12), new List<int>() },
            { Tuple.Create(18,12), new List<int>() },
            { Tuple.Create(19,12), new List<int>() },
            { Tuple.Create(20,12), new List<int>() },
            { Tuple.Create(21,12), new List<int>() },
            { Tuple.Create(22,12), new List<int>() },
            { Tuple.Create(23,12), new List<int>() },
            { Tuple.Create(24,12), new List<int>() },
            { Tuple.Create(25,12), new List<int>() },
            { Tuple.Create(26,12), new List<int>() },
            { Tuple.Create(27,12), new List<int>() },
            { Tuple.Create(28,12), new List<int>() },
            { Tuple.Create(29,12), new List<int>() },
            { Tuple.Create(30,12), new List<int>() },
            { Tuple.Create(31,12), new List<int>() },
        };

        public List<int> ListHolidayingStaffsEmployeeNumbersForDay(int day, int month)
        {
            List<int> employeesOnHoliday;
            if (employeeHolidaysByDate.TryGetValue(Tuple.Create(day, month), out employeesOnHoliday))
                return employeesOnHoliday;
            return new List<int>();
        }
    }
}