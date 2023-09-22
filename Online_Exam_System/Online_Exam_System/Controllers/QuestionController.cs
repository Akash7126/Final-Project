using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Online_Exam_System.Controllers
{
    public class QuestionController : Controller
    {
        private readonly OnlineExamSystemContext _context;
        public QuestionController(OnlineExamSystemContext context)
        {
            _context = context;
        }
        //public IActionResult GetCourseCourseAssign()
        //{
        //    var courseAssigns = (from courseAssign in _context.CourseAssigns
        //                         join course in _context.Courses
        //                         on courseAssign.CourseId equals course.CourseId
        //                         select new { course.CourseId, course.CourseCode }).Distinct().ToList();
        //    ViewBag.CourseAssigns = courseAssigns;
        //    return View();  
        //}
        public IActionResult QuestionBank()
        {
            return View();
        }
        public IActionResult AddQestion()
        {
            var courseAssigns = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns = courseAssigns;
            return View();
        }

        public IActionResult AddMultipleSelectQuestion()
        {
            var courseAssigns1 = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns1 = courseAssigns1;
            return View();
            
        }

        public IActionResult AddTrueFalseQuestion()
        {
            var courseAssigns2 = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns2 = courseAssigns2;
            return View();
        }
    }
}
