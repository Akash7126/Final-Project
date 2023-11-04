using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;
using Online_Exam_System.ViewModel;
using System.Web;
//////+++++++++++++++++++++++++++++
namespace Online_Exam_System.Controllers
{
    public class QuestionController : Controller
    {
        private readonly OnlineExamSystemContext _context;
        private int masterCourseId;
        //   private int courseId;
        public QuestionController(OnlineExamSystemContext context)
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
            var exams = (from e in _context.CreateExams
                         where e.ExamId == examId
                         select new { e.ExamId, e.ExamTitle }).ToList();
            ViewBag.Exams = exams;
            ViewBag.CourseAssigns = courseAssigns;


            var model = new QuestionAnsweViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddQestion(QuestionAnsweViewModel question)
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
            var questionEntity = new Question();
            questionEntity.QuestionDescription = question.QuestionDescription;

            questionEntity.CourseId = question.CourseId;
            questionEntity.TeacherId = teacherId;
            questionEntity.Mark = question.Mark;
            questionEntity.QuestionTypeId = 1;
            questionEntity.ExamId = question.ExamId;
            var courseAssigns = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 where courseAssign.TeacherId == teacherId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();

            var exams = (from e in _context.CreateExams
                         where e.ExamId == question.ExamId
                         select new { e.ExamId, e.ExamTitle }).ToList();
            ViewBag.Exams = exams;
            ViewBag.CourseAssigns = courseAssigns;
            ViewBag.CourseAssigns = courseAssigns;
            _context.Questions.Add(questionEntity);
            _context.SaveChanges();

            var answerList = new List<Answer>();

            var answer1 = new Answer();
            answer1.AnswerText = question.Question1;

            if (question.Question1Answer != null)
            {
                answer1.IsCorrect = true;
            }
            else
            {
                answer1.IsCorrect = false;
            }
            answer1.QuestionId = questionEntity.QuestionId;

            var answer7 = new Answer();
            answer7.AnswerText = question.Question7;

            if (question.Question7Answer != null)
            {
                answer7.IsCorrect = true;
            }
            else
            {
                answer7.IsCorrect = false;
            }
            answer7.QuestionId = questionEntity.QuestionId;

            var answer2 = new Answer();
            answer2.AnswerText = question.Question2;

            if (question.Question2Answer != null)
            {
                answer2.IsCorrect = true;
            }
            else
            {
                answer2.IsCorrect = false;
            }

            answer2.QuestionId = questionEntity.QuestionId;

            var answer3 = new Answer();
            answer3.AnswerText = question.Question3;
            if (question.Question3Answer != null)
            {
                answer3.IsCorrect = true;
            }
            else
            {
                answer3.IsCorrect = false;
            }
            answer3.QuestionId = questionEntity.QuestionId;

            answerList.Add(answer1);
            answerList.Add(answer7);
            answerList.Add(answer2);
            answerList.Add(answer3);

            _context.Answers.AddRange(answerList);
            _context.SaveChanges();

            string currentUrl = HttpContext.Request.Path.ToString() + "?courseId=" + question.CourseId + "&examId=" + question.ExamId;

            //string currentUrl = HttpContext.Request.Path.ToString() + "?courseId=" + question.CourseId;

            //masterCourseId = question.CourseId;

            return Redirect(currentUrl);

        }


        [HttpGet]
        public IActionResult AddMultipleSelectQuestion(int courseId, int examId)
        {
            //var courseAssigns1 = (from courseAssign in _context.CourseAssigns
            //                     join course in _context.Courses
            //                     on courseAssign.CourseId equals course.CourseId
            //                     select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            //ViewBag.CourseAssigns1 = courseAssigns1;
            var userId = HttpContext.Request.Cookies["UserId"];

            var teacherId = _context.Teachers
                .Where(t => t.UserId == userId)
                .Select(t => t.TeacherId)
                .FirstOrDefault();
            var courseAssigns1 = (from courseAssign in _context.CourseAssigns
                                  join course in _context.Courses
                                  on courseAssign.CourseId equals course.CourseId
                                  where courseAssign.TeacherId == teacherId
                                  select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            //var exams = _context.CreateExams.
            var exams = (from e in _context.CreateExams
                         where e.ExamId == examId
                         select new { e.ExamId, e.ExamTitle }).ToList();
            ViewBag.Exams = exams;

            ViewBag.CourseAssigns1 = courseAssigns1;
            var model = new QuestionAnsweViewModel();

            return View(model);

        }

        [HttpPost]
        public IActionResult AddMultipleSelectQuestion(QuestionAnsweViewModel question)
        {
            var userId = HttpContext.Request.Cookies["UserId"];

            var teacherId = _context.Teachers
                .Where(t => t.UserId == userId)
                .Select(t => t.TeacherId)

                .FirstOrDefault();
            var courseAssingCourseid1 = _context.CourseAssigns.FirstOrDefault(s => s.CourseId == question.CourseId);
            // var teacherId = courseAssingCourseid1.TeacherId;

            // answerList.Add(question.editordata1);
            var questionEntity1 = new Question();
            questionEntity1.QuestionDescription = question.QuestionDescription;

            questionEntity1.CourseId = courseAssingCourseid1.CourseId;
            questionEntity1.TeacherId = teacherId;
            questionEntity1.Mark = question.Mark;
            questionEntity1.QuestionTypeId = 2;
            questionEntity1.ExamId = question.ExamId;
            var courseAssigns1 = (from courseAssign in _context.CourseAssigns
                                  join course in _context.Courses
                                  on courseAssign.CourseId equals course.CourseId
                                  where courseAssign.TeacherId == teacherId
                                  select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            var exams = (from e in _context.CreateExams
                         where e.ExamId == question.ExamId
                         select new { e.ExamId, e.ExamTitle }).ToList();
            ViewBag.Exams = exams;

            ViewBag.CourseAssigns1 = courseAssigns1;
            _context.Questions.Add(questionEntity1);
            _context.SaveChanges();


            var answerList = new List<Answer>();
            var answer1 = new Answer();
            answer1.AnswerText = question.Question1;
            ////////////////////////////////////////////////////////
            if (!string.IsNullOrEmpty(question.Question1Answer))
            {
                answer1.IsCorrect = true;
            }
            else
            {
                answer1.IsCorrect = false;
            }
            answer1.QuestionId = questionEntity1.QuestionId;

            var answer7 = new Answer();
            answer7.AnswerText = question.Question7;

            if (!string.IsNullOrEmpty(question.Question7Answer))
            {
                answer7.IsCorrect = true;
            }
            else
            {
                answer7.IsCorrect = false;
            }
            answer7.QuestionId = questionEntity1.QuestionId;

            var answer2 = new Answer();
            answer2.AnswerText = question.Question2;

            if (!string.IsNullOrEmpty(question.Question2Answer))
            {
                answer2.IsCorrect = true;
            }
            else
            {
                answer2.IsCorrect = false;
            }

            answer2.QuestionId = questionEntity1.QuestionId;

            var answer3 = new Answer();
            answer3.AnswerText = question.Question3;
            if (!string.IsNullOrEmpty(question.Question3Answer))
            {
                answer3.IsCorrect = true;
            }
            else
            {
                answer3.IsCorrect = false;
            }
            answer3.QuestionId = questionEntity1.QuestionId;

            answerList.Add(answer1);
            answerList.Add(answer7);
            answerList.Add(answer2);
            answerList.Add(answer3);

            _context.Answers.AddRange(answerList);
            _context.SaveChanges();
            string currentUrl = HttpContext.Request.Path.ToString() + "?courseId=" + question.CourseId + "&examId=" + question.ExamId;
            return Redirect(currentUrl);
        }

        public IActionResult AddTrueFalseQuestion(int courseId, int examId)
        {
            var userId = HttpContext.Request.Cookies["UserId"];

            var teacherId = _context.Teachers
                .Where(t => t.UserId == userId)
                .Select(t => t.TeacherId)
                .FirstOrDefault();
            var courseAssigns2 = (from courseAssign in _context.CourseAssigns
                                  join course in _context.Courses
                                  on courseAssign.CourseId equals course.CourseId
                                  where courseAssign.TeacherId == teacherId
                                  select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            var exams = (from e in _context.CreateExams
                         where e.ExamId == examId
                         select new { e.ExamId, e.ExamTitle }).ToList();
            ViewBag.Exams = exams;
            ViewBag.CourseAssigns2 = courseAssigns2;

            var model = new QuestionAnsweViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddTrueFalseQuestion(QuestionAnsweViewModel question)
        {

            var userId = HttpContext.Request.Cookies["UserId"];

            var teacherId = _context.Teachers
                .Where(t => t.UserId == userId)
                .Select(t => t.TeacherId)
                .FirstOrDefault();

            var courseAssingCourseid1 = _context.CourseAssigns.FirstOrDefault(s => s.CourseId == question.CourseId);
            // var teacherId = courseAssingCourseid1.TeacherId;

            // answerList.Add(question.editordata1);
            var questionEntity1 = new Question();
            questionEntity1.QuestionDescription = question.QuestionDescription;

            questionEntity1.CourseId = courseAssingCourseid1.CourseId;
            questionEntity1.TeacherId = teacherId;
            questionEntity1.Mark = question.Mark;
            questionEntity1.QuestionTypeId = 3;
            questionEntity1.ExamId = question.ExamId;

            var courseAssigns2 = (from courseAssign in _context.CourseAssigns
                                  join course in _context.Courses
                                  on courseAssign.CourseId equals course.CourseId
                                  where courseAssign.TeacherId == teacherId
                                  select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            var exams = (from e in _context.CreateExams
                         where e.ExamId == question.ExamId
                         select new { e.ExamId, e.ExamTitle }).ToList();
            ViewBag.Exams = exams;
            ViewBag.CourseAssigns2 = courseAssigns2;
            _context.Questions.Add(questionEntity1);
            _context.SaveChanges();


            var answerList = new List<Answer>();
            var answer1 = new Answer();
            answer1.AnswerText = question.Question1;
            ////////////////////////////////////////////////////////
            if (question.Question1Answer != null)
            {
                answer1.IsCorrect = true;
            }
            else
            {
                answer1.IsCorrect = false;
            }
            answer1.QuestionId = questionEntity1.QuestionId;

            var answer2 = new Answer();
            answer2.AnswerText = question.Question2;

            if (question.Question2Answer != null)
            {
                answer2.IsCorrect = true;
            }
            else
            {
                answer2.IsCorrect = false;
            }
            answer2.QuestionId = questionEntity1.QuestionId;

            answerList.Add(answer1);
            answerList.Add(answer2);
            _context.Answers.AddRange(answerList);
            _context.SaveChanges();

            string currentUrl = HttpContext.Request.Path.ToString() + "?courseId=" + question.CourseId + "&examId=" + question.ExamId;
            return Redirect(currentUrl);
        }

        public IActionResult DisplayAllExam()
        {
            var userId = HttpContext.Request.Cookies["UserId"];
            var distinctExams = (from ca in _context.CourseAssigns
                                 join q in _context.Questions on ca.CourseId equals q.CourseId
                                 join ce in _context.CreateExams on q.ExamId equals ce.ExamId
                                 join t in _context.Teachers on ca.TeacherId equals t.TeacherId
                                 join c in _context.Courses on q.CourseId equals c.CourseId
                                 join d in _context.Departments on c.DepartmentId equals d.DepartmentId
                                 where t.UserId == userId
                                 select new DisplayExamViewModel
                                 {
                                     ExamId = ce.ExamId,
                                     ExamTitle = ce.ExamTitle,
                                     StartTime = ce.StartTime,
                                     EndTime = ce.EndTime,
                                     CourseTittle = c.CourseTittle,
                                     CourseCode = c.CourseCode,
                                     DepartmentName = d.DepartmentName
                                 }).Distinct().ToList();

            return View(distinctExams);
        }
        public IActionResult DisplayQuestionAnswer(int id)
        {
            var questions = _context.Questions
                                   .Include(q => q.Answers)
                                   .Where(q => q.ExamId == id)
                                   .ToList();

            var exam = _context.CreateExams
                     .FirstOrDefault(e => e.ExamId == id);

            var userId = HttpContext.Request.Cookies["UserId"];
            var distinctExams = (from ca in _context.CourseAssigns
                                 join q in _context.Questions on ca.CourseId equals q.CourseId
                                 join ce in _context.CreateExams on q.ExamId equals ce.ExamId
                                 join t in _context.Teachers on ca.TeacherId equals t.TeacherId
                                 join c in _context.Courses on q.CourseId equals c.CourseId
                                 join d in _context.Departments on c.DepartmentId equals d.DepartmentId
                                 where t.UserId == userId && ce.ExamId == id
                                 select new DisplayExamViewModel
                                 {
                                     ExamId = ce.ExamId,
                                     ExamTitle = ce.ExamTitle,
                                     StartTime = ce.StartTime,
                                     EndTime = ce.EndTime,
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
            var exams = _context.CreateExams.Where(x => x.CourseId == courseId).ToList();
            return Json(exams);
        }
    }
}
