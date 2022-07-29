namespace HumanResources.GlobalMethods
{
    public static class DateMethods
    {
        public static int GetYearDifferenceFromToday(DateTime toDate)
        {
            var today = DateTime.Today;
            var difference = today.Year - toDate.Year;

            if (toDate.Date > today.AddYears(-difference))
            {
                difference--;
            }

            return difference;
        }

        public static int GetDayCountTwoDates(DateTime startDate, DateTime endDate)
        {
            return (int)(endDate - startDate).TotalDays + 1;
        }

        public static List<DateTime> GetDatesBetweenTwoDates(DateTime startDate, DateTime endDate)
        {
            var dates = new List<DateTime>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                dates.Add(date);
            }
            return dates;
        }

        public static List<DateTime> GetWeekDays(List<DateTime> dateList)
        {
            var weekDays = new List<DateTime>();
            foreach (var date in dateList)
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekDays.Add(date);
                }
            }
            return weekDays;
        }
    }
}
