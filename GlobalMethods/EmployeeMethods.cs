namespace HumanResources.GlobalMethods
{
    public static class EmployeeMethods
    {
        public static int AnnualPermitCountSetter(int hireYear)
        {
            var annualPermitCount = 0;

            if (hireYear >= 1 && hireYear <= 5)
            {
                annualPermitCount = 14;
            }
            else if (hireYear > 5 && hireYear < 15)
            {
                annualPermitCount = 20;
            }
            else if (hireYear >= 15)
            {
                annualPermitCount = 26;
            }
            return annualPermitCount;
        }
    }
}
