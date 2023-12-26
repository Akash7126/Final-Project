using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;
using Online_Exam_System.ViewModel;
using System.Linq;

namespace Online_Exam_System.Controllers
{
    public class QuestionBankController : Controller
    {

        private readonly OnlineExamSystemContext _context;
       
        public QuestionBankController(OnlineExamSystemContext context)
        {
            _context = context;
        }




        public IActionResult ExamType()
        {
            return View();
        }


        public IActionResult Departments()
        {
            var departments = _context.Departments.ToList();
            return View(departments);
 
        }


        public IActionResult Courses(int id)
        {
            var Courses = _context.Courses
    .Where(course => course.DepartmentId == id)
    .ToList();
            return View(Courses);
        }



        public IActionResult ExamTitleForAll(int id)
        {

            //var expiredExams = from exam in _context.CreateWrittenExams
            //                   where exam.EndTime < DateTime.Now && exam.CourseId == id
            //                   select exam;

            var query = from exam in _context.CreateWrittenExams
                        where exam.EndTime < DateTime.Now && exam.CourseId == id
                        select new DisplayExamViewModel
                        {
                            ExamId = exam.ExamId,
                            ExamTitle = exam.ExamTitle,
                            Description = exam.Description
                            
                        };
            var examList = query.ToList();
            return View(examList);
        }


        public IActionResult QUestionBankForWritten(int id) 
        {
            var questions = _context.StudentWrittenQuestions
                                   .Where(q => q.ExamId == id)
                                   .ToList();

            var exam = _context.CreateWrittenExams
                     .FirstOrDefault(e => e.ExamId == id);
            var distinctExams = (from ca in _context.CourseAssigns
                                 join q in _context.StudentWrittenQuestions on ca.CourseId equals q.CourseId
                                 join ce in _context.CreateWrittenExams on q.ExamId equals ce.ExamId
                            
                                 join c in _context.Courses on q.CourseId equals c.CourseId
                                 join d in _context.Departments on c.DepartmentId equals d.DepartmentId
                                 where ce.ExamId == id
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
            return View(questions);



        }

        public IActionResult Departmentsmcq()
        {
            var departments = _context.Departments.ToList();
            return View(departments);

        }

        public IActionResult Coursesmcq(int id)
        {
            var Courses = _context.Courses
    .Where(course => course.DepartmentId == id)
    .ToList();
            return View(Courses);
        }



        public IActionResult ExamTitleForAllmcq(int id)
        {

            var query = from exam in _context.CreateExams
                        where exam.EndTime < DateTime.Now && exam.CourseId == id
                        select new DisplayExamViewModel
                        {
                            ExamId = exam.ExamId,
                            ExamTitle = exam.ExamTitle,
                            Description = exam.Description

                        };
            var examList = query.ToList();
            return View(examList);
        }


        public IActionResult QUestionBankFormcq(int id)
        {
            var questions = _context.Questions
                                   .Include(q => q.Answers)
                                   .Where(q => q.ExamId == id)
                                   .ToList();

            var exam = _context.CreateExams
                     .FirstOrDefault(e => e.ExamId == id);

      
            var distinctExams = (from ca in _context.CourseAssigns
                                 join q in _context.Questions on ca.CourseId equals q.CourseId
                                 join ce in _context.CreateExams on q.ExamId equals ce.ExamId
                                
                                 join c in _context.Courses on q.CourseId equals c.CourseId
                                 join d in _context.Departments on c.DepartmentId equals d.DepartmentId
                                 where  ce.ExamId == id
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


    }
}
