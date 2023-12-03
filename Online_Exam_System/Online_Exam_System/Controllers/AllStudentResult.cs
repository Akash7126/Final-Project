using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Online_Exam_System.Controllers
{
    public class AllStudentResult : Controller
    {
        private readonly OnlineExamSystemContext _context;

        public AllStudentResult(OnlineExamSystemContext context)
        {
            _context = context;
        }



        public IActionResult GetStudentResult()
        {
            //var userId = HttpContext.Request.Cookies["UserId"];
            //var teacher = _context.Teachers.FirstOrDefault(s => s.UserId == userId);

            //var teacherId = teacher?.TeacherId;


            //var query = from result in _context.Results
            //            join Student in _context.Students on result.StudentId equals Student.StudentId
            //            join Teacher in _context.Teachers on result.TeacherId equals Teacher.TeacherId
            //            join Question in _context.Questions on result. equals Question.QuestionId
            //            select new
            //            {
            //                QuestionCourseId = question.CourseId,
            //                TeacherName = teacher.TeacherName,
            //                StudentId = student.StudentId,
            //                TotalMarks = result.TotalMarks,
            //                StudentName = student.StudentName
            //            };





            return View();
        }




    }
}
