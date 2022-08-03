using HumanResources.Contexts;
using HumanResources.GlobalMethods;
using HumanResources.Models;
using HumanResources.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanResources.Controllers
{
    public class ReportController : Controller
    {
        private readonly HumanResourcesDbContext _context;

        public ReportController(HumanResourcesDbContext context)
        {
            _context = context;
        }

        public IActionResult ListUserReports()
        {
            var currentUserId = AuthMethods.GetCurrentUserId(User);
            var reportList = _context.Reports.Where(x => x.OwnerId == currentUserId).ToList();
            return View(reportList);
        }

        public IActionResult ListAllUsersReports()
        {
            var reportList = _context.Reports.ToList();

            var userReports = (from report in reportList
                               join user in _context.Users on report.OwnerId equals user.Id
                               select new ReportViewModel()
                               {
                                   Id = report.Id,
                                   OwnerFullName = user.UserName,
                                   Description = report.Description,
                                   Percentage = report.Percentage,
                                   ProjectType = report.ProjectType,
                                   ReportDate = report.ReportDate,
                                   EntryDate = report.EntryDate,
                                   IsLateEntry = report.EntryDate > report.ReportDate
                               }).ToList();

            return View(userReports);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Report report = new();
            return PartialView("_CreateReportModelPartial", report);
        }

        [HttpPost]
        public IActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = AuthMethods.GetCurrentUserId(User);

                report.OwnerId = currentUserId;

                _context.Reports.Add(report);
                _context.SaveChanges();
            }

            return PartialView("_CreateReportModelPartial", report);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var report = _context.Reports.Find(id);
            return PartialView("_EditReportModelPartial", report);
        }

        [HttpPost]
        public IActionResult Edit(Report report)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = AuthMethods.GetCurrentUserId(User);

                report.OwnerId = currentUserId;
                _context.Reports.Update(report);
                _context.SaveChanges();
            }

            return PartialView("_EditReportModelPartial", report);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var deleteReport = _context.Reports.Find(id);

            return PartialView("_DeleteReportModelPartial", deleteReport);
        }

        [HttpPost]
        public IActionResult Delete(Report report)
        {
            if (report != null)
            {
                _context.Reports.Remove(report);
                _context.SaveChanges();
            }
            return PartialView("_DeleteReportModelPartial", report);
        }

        [HttpGet]
        public IActionResult GetAllUsersReports()
        {
            var reportList = _context.Reports.ToList();

            var userReports = (from report in reportList
                               join user in _context.Users on report.OwnerId equals user.Id
                               select new ReportViewModel()
                               {
                                   Id = report.Id,
                                   OwnerFullName = user.UserName,
                                   Description = report.Description,
                                   Percentage = report.Percentage,
                                   ProjectName = report.ProjectName,
                                   ProjectType = report.ProjectType,
                                   ReportDate = report.ReportDate,
                                   EntryDate = report.EntryDate,
                                   IsLateEntry = report.EntryDate > report.ReportDate
                               }).ToList();

            var jsonData = JsonConvert.SerializeObject(userReports);

            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult GetUserReports()
        {

            var currentUserId = AuthMethods.GetCurrentUserId(User);
            var reportList = _context.Reports.Where(x => x.OwnerId == currentUserId).ToList();

            var jsonData = JsonConvert.SerializeObject(reportList);

            return Json(jsonData);
        }
    }
}
