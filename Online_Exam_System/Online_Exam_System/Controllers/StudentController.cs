using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Online_Exam_System.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly OnlineExamSystemContext context;

        public StudentController(ILogger<StudentController> logger, OnlineExamSystemContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult GetAllStudent()
        {
            var data = context.Students.Include(x=>x.Department).Include(x =>x.Batch).ToList(); ///lambda expression

            //var x = (from s in context.Students //LINQ
            //         select new { s.Batch, s.StudentPassword });
            //where s.FirstName == "Rohit"
            //select s).FirstOrDefault();
            //context.Students.Add(sti);
            //context.SaveChanges();

            //context.Students.Remove();
            //context.SaveChanges();
            return View(data);
        }

        //get Student Details//1
        public IActionResult GetStudentDetails(int id)
        {
            var studentById = context.Students.Include(x => x.Department).Include(x => x.Batch).FirstOrDefault(s => s.StudentId == id); ;
            return View(studentById);
        }
    }
}
