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
            
            return View();
        }

        [HttpPost]
        public IActionResult CreateExam(CreateExam createExam)
        {
            CreateExam newExam = new CreateExam();
            newExam.ExamTitle = createExam.ExamTitle;
            newExam.Description= createExam.Description;
            newExam.StartTime = createExam.StartTime;
            newExam.EndTime = createExam.EndTime;

            context.CreateExams.Add(newExam);
            context.SaveChanges();



            
            return View();
        }

        public IActionResult ExamList()
        {
            var exams = context.CreateExams.ToList();
            return View(exams);
        }


        
        public IActionResult UpdateExam(int ExamId)
        {
            // Retrieve the exam from the database based on the examId
            var exam = context.CreateExams.FirstOrDefault(e => e.ExamId == ExamId);
            return View();
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

            // Save changes to the database
            context.SaveChanges();

            // Redirect to the ExamList action to show the updated exam list
            return RedirectToAction("ExamList");
        }

    }



}
