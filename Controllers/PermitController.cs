using HumanResources.Contexts;
using HumanResources.Enums;
using HumanResources.GlobalMethods;
using HumanResources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanResources.Controllers
{
    [Authorize]
    public class PermitController : Controller
    {
        private readonly HumanResourcesDbContext _context;

        public PermitController(HumanResourcesDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var query = (from employee in _context.Employees
                         join permit in _context.Permits on employee.Id equals permit.EmployeeId
                         select new EmployeePermitsViewModel()
                         {
                             Id = employee.Id,
                             EmployeeFullName = employee.FullName,
                             Permits = employee.Permits
                         }).ToList();

            var dropedDuplicatedItems = query.GroupBy(x => x.Id)
                .Select(x => x.First()).ToList();

            return View(dropedDuplicatedItems);
        }

        public IActionResult GetEmployeesWithPermits()
        {
            var query = (from employee in _context.Employees
                         join permit in _context.Permits on employee.Id equals permit.EmployeeId
                         select new EmployeePermitsViewModel()
                         {
                             Id = employee.Id,
                             EmployeeFullName = employee.FullName,
                             RemainPermitCount = employee.RemainPermitCount,
                             Permits = employee.Permits,
                         }).ToList();

            var dropedDuplicatedItems = query.GroupBy(x => x.Id)
                .Select(x => x.First()).ToList();

            var jsonData = JsonConvert.SerializeObject(dropedDuplicatedItems);

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
                var employee = _context.Employees.Find(permit.EmployeeId);

                var dayCount = DateMethods.GetDayCountTwoDates(permit.StartDate, permit.EndDate);

                if (employee != null)
                {
                    _context.Employees.Update(employee);
                    _context.Permits.Add(permit);
                    _context.SaveChanges();

                    if (permit.Type == PermitType.OnLeave)
                    {
                        PermitMethods.RemainPermitCountChecker(_context, permit);
                    }
                }
            }
            return PartialView("_CreatePermitModelPartial", permit);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var findEmployee = _context.Employees.Find(id);
            var query = (from employee in _context.Employees
                         join permit in _context.Permits on employee.Id equals permit.EmployeeId
                         select new EmployeePermitsViewModel()
                         {
                             Id = employee.Id,
                             EmployeeFullName = employee.FullName,
                             Permits = employee.Permits
                         }).ToList();

            return PartialView("_DetailPermitModelPartial", findEmployee);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var permit = _context.Permits.Find(id);
            return PartialView("_EditPermitModelPartial", permit);
        }

        [HttpPost]
        public IActionResult Edit(Permit permit)
        {
            if (ModelState.IsValid)
            {
                _context.Permits.Update(permit);
                _context.SaveChanges();

                PermitMethods.RemainPermitCountChecker(_context, permit);
            }
            return PartialView("_EditPermitModelPartial", permit);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var permit = _context.Permits.Find(id);
            return PartialView("_DeletePermitModelPartial", permit);
        }

        [HttpPost]
        public IActionResult Delete(Permit permit)
        {
            var deletePermit = _context.Permits.Find(permit.Id);

            _context.Permits.Remove(deletePermit);
            _context.SaveChanges();

            if (deletePermit.Type == PermitType.OnLeave)
            {
                PermitMethods.RemainPermitCountChecker(_context, deletePermit);
            }
            return PartialView("_DeletePermitModelPartial", deletePermit);
        }
    }
}
