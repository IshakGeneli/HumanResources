using HumanResources.Contexts;
using HumanResources.Enums;
using HumanResources.Models;

namespace HumanResources.GlobalMethods
{
    public static class PermitMethods
    {
        public static void RemainPermitCountChecker(HumanResourcesDbContext _context, Permit permit)
        {
            var onLeavePermitsOfCurrentUser = (from permit1 in _context.Permits
                                               where permit1.EmployeeId == permit.EmployeeId && permit1.Type == PermitType.OnLeave
                                               select permit1).ToList();

            var permitCount = 0;
            foreach (var permit2 in onLeavePermitsOfCurrentUser)
            {
                var dateList = DateMethods.GetDatesBetweenTwoDates(permit2.StartDate, permit2.EndDate);
                var weekDays = DateMethods.GetWeekDays(dateList);

                permitCount += weekDays.Count;
            }
            var employee = _context.Employees.Find(permit.EmployeeId);

            var hireYear = DateMethods.GetYearDifferenceFromToday(employee.HireDate);
            var annualPermitCount = EmployeeMethods.AnnualPermitCountSetter(hireYear);

            employee.RemainPermitCount = annualPermitCount - permitCount;
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }
    }
}
