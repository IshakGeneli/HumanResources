using HumanResources.Contexts;
using HumanResources.Enums;
using HumanResources.Models;
using HumanResources.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Controllers
{
    public class TaskController : Controller
    {
        private readonly HumanResourcesDbContext _context;

        public TaskController(HumanResourcesDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Models.Task> tasks = _context.Tasks.Include(x => x.Members).ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateTaskViewModel task = new();
            var employeeList = _context.Employees.ToList();
            var selectListItems = new List<SelectListItem>();
            task.MemberIds = employeeList.Select(x => x.Id).ToList();
            foreach (var employee in employeeList)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = employee.FullName,
                    Value = employee.Id.ToString(),
                });
            }
            ViewBag.Members = selectListItems;
            return PartialView("_CreateTaskModelPartial", task);
        }

        [HttpPost]
        public IActionResult Create(CreateTaskViewModel createTask)
        {
            List<Employee> members = new();
            if (createTask.MemberIds != null)
            {
                members = _context.Employees.Where(x => createTask.MemberIds.Contains(x.Id)).ToList();
            }

            Models.Task task = new()
            {
                CreatedDate = DateTime.Now,
                Name = createTask.Name ?? "-",
                Description = createTask.Description,
                Label = TaskLabel.ToDo
            };
            task.Members = members;

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return PartialView("_CreateTaskModelPartial", task);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Models.Task task = _context.Tasks.Include(x => x.Members).SingleOrDefault(x => x.Id == id);
            return PartialView("_DetailTaskModelPartial", task);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            Models.Task task = new();
            return PartialView("_EditTaskModelPartial", task);
        }

        [HttpPost]
        public IActionResult Edit(Models.Task task)
        {
            _context.Add(task);
            _context.SaveChanges();
            return PartialView("_EditTaskModelPartial", task);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Models.Task task = _context.Tasks.Find(id);
            return PartialView("_DeleteTaskModelPartial", task);
        }

        [HttpPost]
        public IActionResult Delete(Models.Task task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return PartialView("_DeleteTaskModelPartial", task);
        }
    }
}
