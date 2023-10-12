using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;
using Online_Exam_System.ViewModel;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Online_Exam_System.Controllers
{
    public class ExamController : Controller
    {
        private readonly ILogger<ExamController> _logger;
        private readonly OnlineExamSystemContext context;

        public ExamController(ILogger<ExamController> logger, OnlineExamSystemContext context)
        {
            _logger = logger;
            this.context = context;
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
        public IActionResult CreateExam(CreateExam createExam)
        {
            var userId = HttpContext.Request.Cookies["UserId"];
            var teacherId = context.Teachers
                                  .Where(t => t.UserId == userId)
                                  .Select(t => t.TeacherId)
                                  .FirstOrDefault();
            CreateExam newExam = new CreateExam();
            newExam.ExamTitle = createExam.ExamTitle;
            newExam.Description= createExam.Description;
            newExam.StartTime = createExam.StartTime;
            newExam.EndTime = createExam.EndTime;
            newExam.TeacherId = teacherId;
            newExam.CourseId =createExam.CourseId;

            context.CreateExams.Add(newExam);
            context.SaveChanges();

            var courseAssigns = (from courseAssign in context.CourseAssigns
                                 join course in context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 where courseAssign.TeacherId == teacherId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns = courseAssigns;

            return RedirectToAction("ExamList", "Exam");
        }

        public IActionResult ExamList()
        {

            var userId = HttpContext.Request.Cookies["UserId"];
            var teacherId = context.Teachers
                                  .Where(t => t.UserId == userId)
                                  .Select(t => t.TeacherId)
                                  .FirstOrDefault();


            // Retrieve exams created by the current teacher based on TeacherId
            var exams = context.CreateExams
                              .Where(e => e.TeacherId == teacherId)
                              .ToList();
            return View(exams);
        }


        
        public IActionResult UpdateExam(int ExamId)
        {
            // Retrieve the exam from the database based on the examId
            var exam = context.CreateExams.FirstOrDefault(e => e.ExamId == ExamId);
            DateTime startTime=exam.StartTime;
            DateTime endTime=exam.EndTime;  
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
            

            var viewModel = new CreateExamViewModel
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
        public IActionResult UpdateExam(CreateExam updatedExam)
        {
            // Retrieve the existing exam from the database based on updatedExam.ExamId
            var existingExam = context.CreateExams.FirstOrDefault(e => e.ExamId == updatedExam.ExamId);

            if (existingExam == null)
            {
                // Exam not found, handle accordingly, e.g., show an error message.
                return RedirectToAction("ExamList");
            }

            // Update the properties of the existing exam with the values from updatedExam
            existingExam.ExamTitle = updatedExam.ExamTitle;
            existingExam.Description = updatedExam.Description;
            existingExam.StartTime = updatedExam.StartTime;
            existingExam.EndTime = updatedExam.EndTime;
            existingExam.CourseId = updatedExam.CourseId;   

            // Save changes to the database
            context.SaveChanges();

            // Redirect to the ExamList action to show the updated exam list
            return RedirectToAction("ExamList", "Exam");
        }

        [HttpPost]
        public IActionResult DeleteExam(int examId)
        {
            // Retrieve the exam from the database based on the examId
            var exam = context.CreateExams.FirstOrDefault(e => e.ExamId == examId);

            if (exam == null)
            {
                // Handle the case where the exam is not found, for example, show an error message.
                return RedirectToAction("ExamList"); // Redirect to the exam list page
            }

            // Remove the exam from the database
            context.CreateExams.Remove(exam);
            context.SaveChanges();

            // Redirect to the exam list page after the deletion
            return RedirectToAction("ExamList", "Exam");
        }
        


    }



}
