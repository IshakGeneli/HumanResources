using HumanResources.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HumanResourcesDbContext _context;

        public EmployeeController(HumanResourcesDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employeeList = _context.Employees.ToList();
            return View(employeeList);
        }
    }
}
