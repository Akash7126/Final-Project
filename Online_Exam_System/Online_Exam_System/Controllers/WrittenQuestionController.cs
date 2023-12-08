using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.ViewModel;

namespace Online_Exam_System.Controllers
{
    public class WrittenQuestionController : Controller
    {
        private readonly OnlineExamSystemContext _context;
        private int masterCourseId;
        //   private int courseId;
        public WrittenQuestionController(OnlineExamSystemContext context)
        {
            _context = context;
        }

        public IActionResult QuestionBank()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddQestion(int courseId, int examId)
        {

            var userId = HttpContext.Request.Cookies["UserId"];

            var teacherId = _context.Teachers
                .Where(t => t.UserId == userId)
                .Select(t => t.TeacherId)
                .FirstOrDefault();

            var courseAssigns = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 where courseAssign.TeacherId == teacherId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();


            // Load exams based on the selected courseId
            var exams = (from e in _context.CreateWrittenExams
                         where e.ExamId == examId
                         select new { e.ExamId, e.ExamTitle }).ToList();
            ViewBag.Exams = exams;
            ViewBag.CourseAssigns = courseAssigns;


            var model = new WrittenQuestionViewModel();

            return View(model);
        }



        [HttpPost]
        public IActionResult AddQestion(WrittenQuestionViewModel question)
        {
            //  courseId = question.CourseId;
            var userId = HttpContext.Request.Cookies["UserId"];

            var teacherId = _context.Teachers
                .Where(t => t.UserId == userId)
                .Select(t => t.TeacherId)
                .FirstOrDefault();


            var courseAssingCourseid = _context.CourseAssigns.FirstOrDefault(s => s.CourseId == question.CourseId);
            //    var  teacherId = courseAssingCourseid.TeacherId;

            // answerList.Add(question.editordata1);
            var questionEntity = new StudentWrittenQuestion();
            questionEntity.QuestionDescription = question.QuestionDescription;

            questionEntity.CourseId = question.CourseId;
            questionEntity.TeacherId = teacherId;
            questionEntity.Mark = question.Mark;
            questionEntity.QuestionTypeId = 4;
            questionEntity.ExamId = question.ExamId;
            var courseAssigns = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 where courseAssign.TeacherId == teacherId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();

            var exams = (from e in _context.CreateWrittenExams
                         where e.ExamId == question.ExamId
                         select new { e.ExamId, e.ExamTitle }).ToList();
            ViewBag.Exams = exams;
            ViewBag.CourseAssigns = courseAssigns;
            ViewBag.CourseAssigns = courseAssigns;
            _context.StudentWrittenQuestions.Add(questionEntity);
            _context.SaveChanges();

           

            string currentUrl = HttpContext.Request.Path.ToString() + "?courseId=" + question.CourseId + "&examId=" + question.ExamId;

            //string currentUrl = HttpContext.Request.Path.ToString() + "?courseId=" + question.CourseId;

            //masterCourseId = question.CourseId;

            return Redirect(currentUrl);

        }


        public IActionResult DisplayAllExam()
        {
            var userId = HttpContext.Request.Cookies["UserId"];
            var distinctExams = (from ca in _context.CourseAssigns
                                 join q in _context.StudentWrittenQuestions on ca.CourseId equals q.CourseId
                                 join ce in _context.CreateWrittenExams on q.ExamId equals ce.ExamId
                                 join t in _context.Teachers on ca.TeacherId equals t.TeacherId
                                 join c in _context.Courses on q.CourseId equals c.CourseId
                                 join d in _context.Departments on c.DepartmentId equals d.DepartmentId
                                 where t.UserId == userId
                                 select new DisplayExamViewModel
                                 {
                                     ExamId = ce.ExamId,
                                     ExamTitle = ce.ExamTitle,
                                     StartTime = ce.StartTime ?? DateTime.MinValue,
                                     EndTime = ce.EndTime ?? DateTime.MinValue,
                                     CourseTittle = c.CourseTittle,
                                     CourseCode = c.CourseCode,
                                     DepartmentName = d.DepartmentName
                                 }).Distinct().ToList();

            return View(distinctExams);
        }

        public IActionResult DisplayQuestion(int id)
        {
            var questions = _context.StudentWrittenQuestions
                                   .Where(q => q.ExamId == id)
                                   .ToList();

            var exam = _context.CreateWrittenExams
                     .FirstOrDefault(e => e.ExamId == id);

            var userId = HttpContext.Request.Cookies["UserId"];
            var distinctExams = (from ca in _context.CourseAssigns
                                 join q in _context.StudentWrittenQuestions on ca.CourseId equals q.CourseId
                                 join ce in _context.CreateWrittenExams on q.ExamId equals ce.ExamId
                                 join t in _context.Teachers on ca.TeacherId equals t.TeacherId
                                 join c in _context.Courses on q.CourseId equals c.CourseId
                                 join d in _context.Departments on c.DepartmentId equals d.DepartmentId
                                 where t.UserId == userId && ce.ExamId == id
                                 select new DisplayExamViewModel
                                 {
                                     ExamId = ce.ExamId,
                                     ExamTitle = ce.ExamTitle,
                                     StartTime = ce.StartTime ?? DateTime.MinValue,
                                     EndTime = ce.EndTime ?? DateTime.MinValue,
                                     CourseTittle = c.CourseTittle,
                                     CourseCode = c.CourseCode,
                                     DepartmentName = d.DepartmentName
                                 }).Distinct().ToList();


            var totalMarks = questions
                .Where(q => q.ExamId == id)
                .Sum(q => q.Mark);

            foreach (var distinctExam in distinctExams)
            {
                var time = distinctExam.EndTime - distinctExam.StartTime;

                int totalMinutes = (int)time.TotalMinutes;
                int hours = totalMinutes / 60;
                int minutes = totalMinutes % 60;

                string formattedTime;

                if (hours > 0)
                {
                    formattedTime = $"{hours}hr {minutes}min";
                }
                else
                {
                    formattedTime = $"{minutes}min";
                }

                ViewBag.Time = formattedTime;
            }

            ViewBag.ExamEndTime = exam.EndTime;
            ViewBag.DistinctExams = distinctExams;
            ViewBag.TotalMarks = totalMarks;


            // Pass the questions and the exam status to the view

            return View(questions);

        }
        public JsonResult GetExamsByCourse(int courseId)
        {
            var exams = _context.CreateWrittenExams.Where(x => x.CourseId == courseId).ToList();
            return Json(exams);
        }
    }
}
