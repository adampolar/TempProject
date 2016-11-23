using System.Collections.Generic;

namespace HolidayService
{
    public interface IHolidayRepository
    {
        List<int> ListHolidayingStaffsEmployeeNumbersForDay(int day, int month);
    }
}