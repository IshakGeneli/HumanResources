using HumanResources.Contexts;
using HumanResources.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Index()
        {
            var employeeList = _context.Employees.ToList();
            return View(employeeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Employee employee = new();
            return PartialView("_CreateEmployeeModelPartial", employee);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
            return PartialView("_CreateEmployeeModelPartial", employee);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id);
            return PartialView("_EditEmployeeModelPartial", employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
            }
            return PartialView("_EditEmployeeModelPartial", employee);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            return PartialView("_DeleteEmployeeModelPartial", employee);
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            var deleteEmployee = _context.Employees.Find(employee.Id);
            _context.Employees.Remove(deleteEmployee);
            _context.SaveChanges();
            return PartialView("_DeleteEmployeeModelPartial", deleteEmployee);
        }
    }
}
