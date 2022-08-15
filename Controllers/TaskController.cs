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
            if (ModelState.IsValid)
            {
                List<Employee> members = new();
                if (createTask.MemberIds != null)
                {
                    members = _context.Employees.Where(x => createTask.MemberIds.Contains(x.Id)).ToList();
                }

                Models.Task task = new()
                {
                    CreatedDate = DateTime.Now,
                    Name = createTask.Name,
                    Description = createTask.Description,
                    Label = TaskLabel.ToDo
                };
                task.Members = members;

                _context.Tasks.Add(task);
                _context.SaveChanges();
            }
            return PartialView("_CreateTaskModelPartial", createTask);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Models.Task task = _context.Tasks.Include(x => x.Members).SingleOrDefault(x => x.Id == id);
            return PartialView("_DetailTaskModelPartial", task);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Models.Task task = _context.Tasks.Include(x => x.Members).FirstOrDefault(x => x.Id == id);
            EditTaskViewModel editTask = new();
            if (task != null)
            {
                editTask = new()
                {
                    Description = task.Description,
                    Name = task.Name,
                    Label = task.Label,
                    MemberIds = task.Members.Select(x => x.Id).ToList(),
                };
                var selectListItems = new List<SelectListItem>();
                var employeeList = _context.Employees.ToList();
                foreach (var employee in employeeList)
                {
                    selectListItems.Add(new SelectListItem
                    {
                        Text = employee.FullName,
                        Value = employee.Id.ToString(),
                    });
                }
                ViewBag.Members = selectListItems;
            }
            return PartialView("_EditTaskModelPartial", editTask);
        }

        [HttpPost]
        public IActionResult Edit(EditTaskViewModel editTask)
        {
            if (ModelState.IsValid)
            {
                var currentTask = _context.Tasks.Include(x => x.Members).FirstOrDefault(x => x.Id == editTask.Id);
                currentTask.Name = editTask.Name;
                currentTask.Description = editTask.Description;
                currentTask.Label = editTask.Label;

                List<Employee> members = new();
                if (editTask.MemberIds != null)
                {
                    members = _context.Employees.Where(x => editTask.MemberIds.Contains(x.Id)).ToList();
                }

                currentTask.Members.Clear();
                currentTask.Members.AddRange(members);

                _context.Tasks.Update(currentTask);
                _context.SaveChanges();
            }
            return PartialView("_EditTaskModelPartial", editTask);
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
