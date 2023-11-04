using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;

namespace Online_Exam_System.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly OnlineExamSystemContext context;

        public DepartmentController(ILogger<DepartmentController> logger, OnlineExamSystemContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult GetAllDepartment()
        {
            var data = context.Departments.ToList();
            return View(data);
        }

        //create Department
        public IActionResult AddDepartment()
        {
            return View();
        }
        //create Department
        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();

            return RedirectToAction("GetAllDepartment", "Department");
        }

        //Delete
        public IActionResult DeleteDepartment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteDepartment(int newId)
        {
            var aDepartment = context.Departments.Find(newId);
            context.Departments.Remove(aDepartment);
            context.SaveChanges();
            return RedirectToAction("GetAllDepartment", "Department");
        }
    }
}
