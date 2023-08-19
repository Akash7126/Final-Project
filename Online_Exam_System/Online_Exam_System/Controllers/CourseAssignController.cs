using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;
using Online_Exam_System.ViewModel;

namespace Online_Exam_System.Controllers
{
    public class CourseAssignController : Controller
    {
        private readonly ILogger<CourseAssignController> _logger;
        private readonly OnlineExamSystemContext context;

        public CourseAssignController(ILogger<CourseAssignController> logger, OnlineExamSystemContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult CourseAssignToStudent()
        {
            var departments = context.Departments.ToList();
            var courses = context.Courses.ToList();///lambda expression
            var teachers = context.Teachers.ToList();
            ViewBag.Departments = departments;
            ViewBag.Courses = courses;
            ViewBag.Teachers = teachers;
            return View();
        }
        [HttpPost]
        public ActionResult CourseAssignToStudent(CourseAssignViewModel courseAssign)
        {
            if (ModelState.IsValid) // Ensure the data is valid
            {

                foreach(var studentId in courseAssign.StudentList)
                {
                    var courseAssignEntity = new CourseAssign
                    {
                        DepartmentId = courseAssign.DepartmentId,
                        CourseId = courseAssign.CourseId,
                        TeacherId = courseAssign.TeacherId,
                        BatchId = courseAssign.BatchId,
                        StudentId= studentId
                        // Set other properties as needed
                    };
                    context.CourseAssigns.Add(courseAssignEntity);

                }
                
                context.SaveChanges();
                return RedirectToAction("CourseAssign", "CourseAssignToStudent");
            }
            var departments = context.Departments.ToList();
            var courses = context.Courses.ToList();///lambda expression
            var teachers = context.Teachers.ToList();
            ViewBag.Departments = departments;
            ViewBag.Courses = courses;
            ViewBag.Teachers = teachers;
            return View();
        }

        public JsonResult GetCourses(int departmentId)
        {
            var data = context.Courses.Where(x => x.DepartmentId == departmentId).ToList();
            return new JsonResult(data);
        }

        public JsonResult GetTeachers(int departmentId)
        {
            var data = context.Teachers.Where(x => x.DepartmentId == departmentId).ToList();
            return new JsonResult(data);
        }

        public JsonResult GetBatch(int departmentId)
        {
            var data = context.Batches.Where(x => x.DepartmentId == departmentId).ToList();
            return new JsonResult(data);
        }

        [HttpGet]
        public IActionResult GetStudentsByBatch(int batch)
        {
            var students = context.Students.Where(s => s.BatchId == batch).ToList();
            return Json(students); // Return students as JSON
        }
    }
}
