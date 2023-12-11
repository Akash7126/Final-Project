using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.ViewModel;

namespace Online_Exam_System.Controllers
{
    public class WrittenExamController : Controller
    {
        private readonly ILogger<WrittenExamController> _logger;
        private readonly OnlineExamSystemContext context;

        public WrittenExamController(ILogger<WrittenExamController> logger, OnlineExamSystemContext context)
        {
            _logger = logger;
            this.context = context;
        }
        private bool IsExamOver(DateTime examEndTime)
        {
            return DateTime.Now > examEndTime;
        }
        public IActionResult CreateExam()
        {

            var userId = HttpContext.Request.Cookies["UserId"];

            var teacherId = context.Teachers
                .Where(t => t.UserId == userId)
                .Select(t => t.TeacherId)
                .FirstOrDefault();
            var courseAssigns = (from courseAssign in context.CourseAssigns
                                 join course in context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 where courseAssign.TeacherId == teacherId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns = courseAssigns;
            return View();
        }


        [HttpPost]
        public IActionResult CreateExam(CreateWrittenExam createWrittenExam)
        {
            var userId = HttpContext.Request.Cookies["UserId"];
            var teacherId = context.Teachers
                                  .Where(t => t.UserId == userId)
                                  .Select(t => t.TeacherId)
                                  .FirstOrDefault();
            CreateWrittenExam newExam = new CreateWrittenExam();
            newExam.ExamTitle = createWrittenExam.ExamTitle;
            newExam.Description = createWrittenExam.Description;
            newExam.StartTime = createWrittenExam.StartTime;
            newExam.EndTime = createWrittenExam.EndTime;
            newExam.TeacherId = teacherId;
            newExam.CourseId = createWrittenExam.CourseId;

            context.CreateWrittenExams.Add(newExam);
            context.SaveChanges();

            var courseAssigns = (from courseAssign in context.CourseAssigns
                                 join course in context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 where courseAssign.TeacherId == teacherId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns = courseAssigns;

            return RedirectToAction("ExamList");
        }


        public IActionResult ExamList()
        {

            var userId = HttpContext.Request.Cookies["UserId"];
            var teacherId = context.Teachers
                                  .Where(t => t.UserId == userId)
                                  .Select(t => t.TeacherId)
                                  .FirstOrDefault();


            // Retrieve exams created by the current teacher based on TeacherId
            var exams = context.CreateWrittenExams
                              .Where(e => e.TeacherId == teacherId)
                              .ToList();
            return View(exams);
        }


        public IActionResult UpdateExam(int ExamId)
        {
            // Retrieve the exam from the database based on the examId
            var exam = context.CreateWrittenExams.FirstOrDefault(e => e.ExamId == ExamId);
            DateTime startTime = exam.StartTime ?? DateTime.MinValue;
            DateTime endTime = exam.EndTime ?? DateTime.MinValue;

            var description = exam.Description;
            var examTitle = exam.ExamTitle;
            var courseId = exam.CourseId;
            var userId = HttpContext.Request.Cookies["UserId"];

            var teacherId = context.Teachers
                .Where(t => t.UserId == userId)
                .Select(t => t.TeacherId)
                .FirstOrDefault();
            var courseAssigns = (from courseAssign in context.CourseAssigns
                                 join course in context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 where courseAssign.TeacherId == teacherId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();


            var viewModel = new CreateWrittenExamViewModel
            {
                StartTime = startTime,
                EndTime = endTime,
                Description = description,
                ExamTitle = examTitle,
                CourseId = courseId
                // Set additional properties if needed
            };
            ViewBag.CourseAssigns = courseAssigns;
            // Pass the view model to the view for editing
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult UpdateExam(CreateWrittenExam updatedExam)
        {
            // Retrieve the existing exam from the database based on updatedExam.ExamId
            var existingExam = context.CreateWrittenExams.FirstOrDefault(e => e.ExamId == updatedExam.ExamId);


            if (existingExam == null)
            {
                // Exam not found, handle accordingly, e.g., show an error message.
                return RedirectToAction("ExamList");
            }
            // Update the properties of the existing exam with the values from updatedExam
            existingExam.ExamTitle = updatedExam.ExamTitle;
            existingExam.Description = updatedExam.Description;
            existingExam.StartTime = updatedExam.StartTime ?? DateTime.MinValue;
            existingExam.EndTime = updatedExam.EndTime ?? DateTime.MinValue;
            existingExam.CourseId = updatedExam.CourseId;

            // Save changes to the database
            context.SaveChanges();
            

            // Redirect to the ExamList action to show the updated exam list
            return RedirectToAction("ExamList");
        }


        [HttpPost]
        public IActionResult DeleteExam(int examId)
        {
            // Retrieve the exam from the database based on the examId
            var exam = context.CreateWrittenExams.FirstOrDefault(e => e.ExamId == examId);

            if (exam == null)
            {
                // Handle the case where the exam is not found, for example, show an error message.
                return RedirectToAction("ExamList"); // Redirect to the exam list page
            }

            // Remove the exam from the database
            context.CreateWrittenExams.Remove(exam);
            context.SaveChanges();

            // Redirect to the exam list page after the deletion
            return RedirectToAction("ExamList");
        }


        public IActionResult GiveWrittenExam()
        {
            var userId = HttpContext.Request.Cookies["UserId"];
            var distinctExams = (from ca in context.CourseAssigns
                                 join q in context.StudentWrittenQuestions on ca.CourseId equals q.CourseId
                                 join ce in context.CreateWrittenExams on q.ExamId equals ce.ExamId
                                 join s in context.Students on ca.StudentId equals s.StudentId
                                 join c in context.Courses on q.CourseId equals c.CourseId
                                 join d in context.Departments on c.DepartmentId equals d.DepartmentId
                                 where s.UserId == userId
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



        public IActionResult WrittenQuestionforStudent(int id)
        {
            var questions = context.StudentWrittenQuestions
                                   .Where(q => q.ExamId == id)
                                   .ToList();

            var teacherId = context.StudentWrittenQuestions
                        .Where(q => q.ExamId == id)
                        .Select(q => q.TeacherId)
                        .FirstOrDefault();


            var exam = context.CreateWrittenExams
                     .FirstOrDefault(e => e.ExamId == id);


            var userId = HttpContext.Request.Cookies["UserId"];
            var student = context.Students.FirstOrDefault(s => s.UserId == userId);

            var studentId = student?.StudentId;
            var distinctExams = (from ca in context.CourseAssigns
                                 join q in context.StudentWrittenQuestions on ca.CourseId equals q.CourseId
                                 join ce in context.CreateWrittenExams on q.ExamId equals ce.ExamId
                                 join s in context.Students on ca.StudentId equals s.StudentId
                                 join c in context.Courses on q.CourseId equals c.CourseId
                                 join d in context.Departments on c.DepartmentId equals d.DepartmentId
                                 where s.UserId == userId && ce.ExamId == id
                                 select new DisplayWrittenExamViewModel
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


            var totalQuestions = context.StudentWrittenQuestions
                .Count(q => q.ExamId == id);

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
            ViewBag.NewExamid = id;
            ViewBag.TeacherId = teacherId;
            // Check if the exam is over based on the ExamEndTime
            bool isExamOver = IsExamOver(exam.EndTime ?? DateTime.MinValue);
            ViewBag.IsExamOver = isExamOver;
            ViewBag.DistinctExams = distinctExams;
            ViewBag.TotalMarks = totalMarks;
            ViewBag.TotalQuestions = totalQuestions;
            ViewBag.StudentId = studentId;

            return View(questions);
        }



        public IActionResult ExamOver(int id)
        {
            var questions = context.StudentWrittenQuestions
                                  
                                   .Where(q => q.ExamId == id)
                                   .ToList();

            var teacherId = context.StudentWrittenQuestions
                        .Where(q => q.ExamId == id)
                        .Select(q => q.TeacherId)
                        .FirstOrDefault();


            var exam = context.CreateWrittenExams
                     .FirstOrDefault(e => e.ExamId == id);


            var userId = HttpContext.Request.Cookies["UserId"];
            var distinctExams = (from ca in context.CourseAssigns
                                 join q in context.StudentWrittenQuestions on ca.CourseId equals q.CourseId
                                 join ce in context.CreateWrittenExams on q.ExamId equals ce.ExamId
                                 join s in context.Students on ca.StudentId equals s.StudentId
                                 join c in context.Courses on q.CourseId equals c.CourseId
                                 join d in context.Departments on c.DepartmentId equals d.DepartmentId
                                 where s.UserId == userId && ce.ExamId == id
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

            var totalQuestions = context.Questions
                .Count(q => q.ExamId == id);

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

            ViewBag.ExamEndTime = exam.EndTime ?? DateTime.MinValue;
            ViewBag.NewExamid = id;
            ViewBag.TeacherId = teacherId;
            // Check if the exam is over based on the ExamEndTime
            bool isExamOver = IsExamOver(exam.EndTime ?? DateTime.MinValue);
            ViewBag.IsExamOver = isExamOver;
            ViewBag.DistinctExams = distinctExams;
            ViewBag.TotalQuestions = totalQuestions;
            return View(questions);
        }




    }
}
