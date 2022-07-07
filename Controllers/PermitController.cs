using HumanResources.Contexts;
using HumanResources.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanResources.Controllers
{
    public class PermitController : Controller
    {
        private readonly HumanResourcesDbContext _context;

        public PermitController(HumanResourcesDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var query2 = (from employee in _context.Employees
                          join permit in _context.Permits on employee.Id equals permit.EmployeeId
                          select new EmployeePermitsViewModel()
                          {
                              Id = employee.Id,
                              EmployeeFullName = employee.FullName,
                              Permits = employee.Permits
                          }).ToList();

            var dropedDupicatedItems = query2.GroupBy(x => x.Id)
                .Select(x => x.First()).ToList();

            return View(dropedDupicatedItems);
        }

        public IActionResult GetEmployeesWithPermits()
        {
            var query2 = (from employee in _context.Employees
                          join permit in _context.Permits on employee.Id equals permit.EmployeeId
                          select new EmployeePermitsViewModel()
                          {
                              Id = employee.Id,
                              EmployeeFullName = employee.FullName,
                              Permits = employee.Permits
                          }).ToList();

            var jsonData = JsonConvert.SerializeObject(query2);

            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var permit = new Permit();

            var employees = _context.Employees.ToList();
            ViewBag.EmployeeList = employees;
            return PartialView("_CreatePermitModelPartial", permit);
        }

        [HttpPost]
        public IActionResult Create(Permit permit)
        {
            var employees = _context.Employees.ToList();
            ViewBag.EmployeeList = employees;
            if (ModelState.IsValid)
            {
                _context.Permits.Add(permit);
                _context.SaveChanges();
            }
            return PartialView("_CreatePermitModelPartial", permit);
        }

    }
}
