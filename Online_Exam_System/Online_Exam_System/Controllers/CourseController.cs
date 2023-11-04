using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;

namespace Online_Exam_System.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;
        private readonly OnlineExamSystemContext context;

        public CourseController(ILogger<CourseController> logger, OnlineExamSystemContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult GetAllCourse()
        {
            var data = context.Courses.Include(x => x.Department).ToList(); ///lambda expression
            return View(data);
        }

        //Add Course
        public IActionResult AddCourse()
        {
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;
            return View();
        }
        //Create course
        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();

            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;


            return RedirectToAction("GetAllCourse", "Course");
        }


        //Update or Edit Course
        public IActionResult UpdateCourseDetails(int id)
        {
            Course Acourse = context.Courses.FirstOrDefault(x => x.CourseId == id);
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;
            return View(Acourse);
        }

        [HttpPost]
        public IActionResult UpdateCourseDetails(int id, Course course)
        {
            var aCourse = context.Courses.Find(id);

            aCourse.CourseCode = course.CourseCode;
            aCourse.DepartmentId = course.DepartmentId;
            aCourse.CourseTittle = course.CourseTittle;
            aCourse.CourseCredit = course.CourseCredit;
            context.Courses.Update(aCourse);
            context.SaveChanges();

            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;
            return RedirectToAction("GetAllCourse", "Course");
        }

        //Delete Course
        public IActionResult DeleteCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteCourse(int newId)
        {
            var aCourse = context.Courses.Find(newId);
            context.Courses.Remove(aCourse);
            context.SaveChanges();
            return RedirectToAction("GetAllCourse", "Course");
        }
    }
}
