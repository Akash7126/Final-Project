using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Models;
using Online_Exam_System.ViewModel;

namespace Online_Exam_System.Controllers
{
    public class ResultForStudentController : Controller
    {
        private readonly OnlineExamSystemContext _context;

        public ResultForStudentController(OnlineExamSystemContext context)
        {
            _context = context;
        }


        public IActionResult GetCreatedCoursesExam()
        {
            //var userId = "Mrinmoy Biswas Akash1aa125";
            var userId = HttpContext.Request.Cookies["UserId"];

            var distinctCourseIds = (from ca in _context.CourseAssigns
                                     join q in _context.Questions on ca.CourseId equals q.CourseId
                                     join ce in _context.CreateExams on q.ExamId equals ce.ExamId
                                     join t in _context.Students on ca.StudentId equals t.StudentId
                                     join c in _context.Courses on ca.CourseId equals c.CourseId
                                     where t.UserId == userId
                                     select new DisplayExamViewModel
                                     {
                                         CourseId = c.CourseId,
                                         CourseTittle = c.CourseTittle // Assuming CourseTittle is the property for the course title
                                     }).Distinct().ToList();

            return View(distinctCourseIds);


        }


        public IActionResult ExamTitleS(int id)
        {

            var userId = HttpContext.Request.Cookies["UserId"];
            var student = _context.Students.FirstOrDefault(s => s.UserId == userId);
            var studentId = student.StudentId;

            var distinctExams = (from ca in _context.CourseAssigns
                                 join q in _context.Questions on ca.CourseId equals q.CourseId
                                 join ce in _context.CreateExams on q.ExamId equals ce.ExamId
                                 join s in _context.Students on ca.StudentId equals s.StudentId
                                 join c in _context.Courses on q.CourseId equals c.CourseId
                                 join d in _context.Departments on c.DepartmentId equals d.DepartmentId
                                 where s.StudentId == studentId && c.CourseId == id
                                 select new DisplayExamViewModel
                                 {
                                     ExamId = ce.ExamId,
                                     ExamTitle = ce.ExamTitle,
                                     StartTime = ce.StartTime,
                                     EndTime = ce.EndTime,
                                     CourseTittle = c.CourseTittle,
                                     CourseCode = c.CourseCode,
                                     DepartmentName = d.DepartmentName,
                                     StudentId = studentId

                                 }).Distinct().ToList();




            return View(distinctExams);
           
        }

        public IActionResult GetResult(int studentId, int id)
        {
            var query = from results in _context.Results
                        join student in _context.Students on results.StudentId equals student.StudentId
                        where results.StudentId == studentId && results.ExamId == id
                        select new StudentAllResultsViewModel
                        {
                            TottalMarks = results.TottalMarks,
                            StudentName = student.StudentName,
                            UserId = student.UserId,
                            StudentId = studentId,
                            ExamId = id

                        };

            var result = query.ToList();
            return View(result);
        }





    }
}
