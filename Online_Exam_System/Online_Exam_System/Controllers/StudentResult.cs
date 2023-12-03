using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Online_Exam_System.Controllers
{
    public class StudentResult : Controller
    {
        private readonly OnlineExamSystemContext _context;

        public StudentResult(OnlineExamSystemContext context)
        {
            _context = context;
        }
        

        public IActionResult GetStudentsResul()
        {
            var results = (
            from result in _context.Results
                join student in _context.Students on result.StudentId equals student.StudentId
                join teacher in _context.Teachers on result.TeacherId equals teacher.TeacherId
                join question in _context.Questions on result.ExamId equals question.ExamId
                select new
                {
                    question.CourseId,
                    teacher.TeacherName,
                    student.StudentId,
                    result.TottalMarks,
                    student.StudentName
                }
            ).Distinct().ToList();

            return View(results);
            
        }

    }
}
