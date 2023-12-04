using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;
using Online_Exam_System.ViewModel;

namespace Online_Exam_System.Controllers
{
    public class StudentResultController : Controller
    {
        private readonly OnlineExamSystemContext _context;

        public StudentResultController(OnlineExamSystemContext context)
        {
            _context = context;
        }


        public IActionResult GetCreatedExam()
        {
            //var userId = "Mrinmoy Biswas Akash1aa125";
            var userId = HttpContext.Request.Cookies["UserId"];

            var distinctCourseIds = (from ca in _context.CourseAssigns
                                     join q in _context.Questions on ca.CourseId equals q.CourseId
                                     join ce in _context.CreateExams on q.ExamId equals ce.ExamId
                                     join t in _context.Teachers on ca.TeacherId equals t.TeacherId
                                     join c in _context.Courses on ca.CourseId equals c.CourseId
                                     where t.UserId == userId
                                     select new DisplayExamViewModel
                                     {
                                         CourseId = c.CourseId,
                                         CourseTittle = c.CourseTittle // Assuming CourseTittle is the property for the course title
                                     }).Distinct().ToList();

            return View(distinctCourseIds);
          

        }

        public IActionResult ExamTitle(int id)
        {

            //var userId = "Mrinmoy Biswas Akash1aa125";
            var userId = HttpContext.Request.Cookies["UserId"];
            var teaxher = _context.Teachers.FirstOrDefault(s => s.UserId == userId);

            var teacherId = teaxher?.TeacherId;
            //var teacherId = 1010;
            var query = from exam in _context.CreateExams
                        where exam.CourseId == id && exam.TeacherId == teacherId
                        select new DisplayExamViewModel
                        {
                            ExamId = exam.ExamId,
                            ExamTitle=exam.ExamTitle,
                            Description=exam.Description,
                            TeacherId = (int)exam.TeacherId
                        };
            var examList = query.ToList();
            return View(examList);
            
        }


        public IActionResult StudentsResult(int teacherId, int id)
        {
            var query = from results in _context.Results
                        join student in _context.Students on results.StudentId equals student.StudentId
                        where results.TeacherId == teacherId && results.ExamId == id
                        select new StudentAllResultsViewModel
                        {
                            TottalMarks= results.TottalMarks,
                            StudentName = student.StudentName,
                            UserId = student.UserId
                        };

            var result = query.ToList();
            return View(result);  
        }



    }
}
