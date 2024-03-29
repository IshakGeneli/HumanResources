﻿using ExcelDataReader;
using HumanResources.Contexts;
using HumanResources.GlobalMethods;
using HumanResources.Identity;
using HumanResources.Models;
using HumanResources.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace HumanResources.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HumanResourcesDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public EmployeeController(HumanResourcesDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var employeeList = _context.Employees.ToList();

            var employeeViewModelList = new List<EmployeeViewModel>();

            foreach (var employee in employeeList)
            {
                var age = DateMethods.GetYearDifferenceFromToday(employee.BirthDate);

                var hireYear = DateMethods.GetYearDifferenceFromToday(employee.HireDate);

                var annualPermitCount = EmployeeMethods.AnnualPermitCountSetter(hireYear);

                var employeeViewModel = new EmployeeViewModel()
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    HireDate = employee.HireDate,
                    Department = employee.Department,
                    Age = age,
                    AnnualPermitCount = annualPermitCount,
                    RemainPermitCount = employee.RemainPermitCount,
                };

                employeeViewModelList.Add(employeeViewModel);
            }

            return View(employeeViewModelList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Employee employee = new();
            return PartialView("_CreateEmployeeModelPartial", employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var hireYear = DateMethods.GetYearDifferenceFromToday(employee.HireDate);

                var remainPermitCount = 0;

                if (hireYear >= 1 && hireYear <= 5)
                {
                    remainPermitCount = 14;
                }
                else if (hireYear > 5 && hireYear < 15)
                {
                    remainPermitCount = 20;
                }
                else if (hireYear >= 15)
                {
                    remainPermitCount = 26;
                }

                employee.RemainPermitCount = remainPermitCount;

                var formatFullName = $"{employee.FullName.ToLower().Replace(" ", "")}";
                var formatDate = $"{ DateTime.Now.ToString().Substring(0, 10).Replace(".", "").Replace(" ", "")}";

                var user = new AppUser
                {
                    UserName = $"{formatFullName}_{formatDate}" +
                               $"",
                    Email = $"{formatFullName}@a.com"
                };
                await _userManager.CreateAsync(user, password: "123456");

                employee.UserId = user.Id;

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
        public async Task<IActionResult> Delete(Employee employee)
        {
            var deleteEmployee = _context.Employees.Find(employee.Id);

            _context.Employees.Remove(deleteEmployee);
            _context.SaveChanges();

            var user = await _userManager.FindByIdAsync(deleteEmployee.UserId);

            await _userManager.DeleteAsync(user);

            return PartialView("_DeleteEmployeeModelPartial", deleteEmployee);
        }

        public IActionResult GetEmployees()
        {
            var employeeList = _context.Employees.ToList();

            var jsonResult = JsonConvert.SerializeObject(employeeList);

            return Json(jsonResult);
        }

        public IActionResult GetEmployeeById(int id)
        {
            var employee = _context.Employees.Find(id);

            var jsonResult = JsonConvert.SerializeObject(employee);

            return Json(jsonResult);
        }

        public IActionResult UploadDownloadExcelModal()
        {
            return PartialView("_EmployeeExcelPartial");
        }

        public IActionResult DownloadEmployeeExcelTemplate([FromServices] IWebHostEnvironment hostEnvironment)
        {
            var fileName = "toplu-personel-ekle-taslak.xlsx";
            var filePath = $"{hostEnvironment.WebRootPath}\\files\\excel-files";
            var memory = DownloadFile(fileName, filePath);

            return File(memory.ToArray(), "application/vnd.ms-excel", fileName);
        }

        private MemoryStream DownloadFile(string filename, string uploadPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, filename);
            var memory = new MemoryStream();

            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;

            return memory;
        }

        [HttpPost]
        public async Task<IActionResult> HandleExcelFile(IFormFile excelFile, [FromServices] IWebHostEnvironment hostEnvironment)
        {
            string filePath = $"{hostEnvironment.WebRootPath}\\files\\temp-excel-files\\{excelFile.FileName}";
            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                excelFile.CopyTo(fileStream);
                fileStream.Flush();
            }

            var employees = new List<Employee>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var excelReader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataset = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    var table = dataset.Tables;

                    var resultTable = table["Sheet1"];

                    foreach (DataRow row in resultTable.Rows)
                    {
                        var a = row[0];
                        var formatFullName = row[0].ToString().ToLower()
                            .Replace(" ", "")
                            .Replace("ç", "c")
                            .Replace("ğ", "g")
                            .Replace("ı", "i")
                            .Replace("ş", "s")
                            .Replace("ü", "o")
                            .Replace("ö", "o")
                            .Replace("ö", "o");
                        var formatDate = $"{ DateTime.Now.ToString().Substring(0, 10).Replace(".", "").Replace(" ", "")}";

                        var user = new AppUser
                        {
                            UserName = $"{formatFullName}_{formatDate}" +
                                       $"",
                            Email = $"{formatFullName}@a.com"
                        };
                        await _userManager.CreateAsync(user, password: "123456");

                        employees.Add(new Employee()
                        {
                            FullName = row[0].ToString(),
                            Department = row[1].ToString(),
                            BirthDate = (DateTime)row[2],
                            HireDate = (DateTime)row[3],
                            UserId = user.Id,
                        });
                    }

                }
            }

            System.IO.File.Delete(filePath);

            _context.Employees.AddRange(employees);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
