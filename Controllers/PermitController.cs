using HumanResources.Contexts;
using HumanResources.Enums;
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
            var permitViewModels = new List<PermitViewModel>();
            var query = from permit in _context.Permits
                        join employee in _context.Employees on permit.EmployeeId equals employee.Id
                        select new PermitViewModel()
                        {
                            EmployeeFullName = employee.FullName,
                            StartDate = permit.StartDate,
                            EndDate = permit.EndDate,
                            PermitType = permit.Type == PermitType.Excused ? "Mazeretli" : "Mazeretsiz"
                        };

            var model = query.ToList();

            return View(model);
        }

        public IActionResult GetPermits()
        {
            var query = from permit in _context.Permits
                        join employee in _context.Employees on permit.EmployeeId equals employee.Id
                        select new PermitViewModel()
                        {
                            EmployeeFullName = employee.FullName,
                            StartDate = permit.StartDate,
                            EndDate = permit.EndDate,
                            PermitType = permit.Type == PermitType.Excused ? "Mazeretli" : "Mazeretsiz"
                        };

            var model = query.ToList();

            var jsonData = JsonConvert.SerializeObject(model);

            return Json(jsonData);
        }
    }
}
