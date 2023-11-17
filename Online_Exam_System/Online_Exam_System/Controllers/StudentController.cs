using Humanizer;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using NuGet.DependencyResolver;

using Online_Exam_System.Models;

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
            var data = context.Students.Include(x => x.Department).Include(x => x.Batch).ToList(); ///lambda expression
            return View(data);
        }

        //get Student Details//1
        public IActionResult GetStudentDetails(int id)
        {
            var studentById = context.Students.Include(x => x.Department).Include(x => x.Batch).FirstOrDefault(s => s.StudentId == id); ;
            return View(studentById);
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("UITS Authority", "akash.ghosh1700@gmail.com"));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("akash.ghosh1700@gmail.com", "hmhxrzzdebuibery");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        public IActionResult AddStudent()
        {
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            var batches = context.Batches.ToList();

            ViewBag.Departments = departments;
            ViewBag.Batches = batches;
            return View();
        }
        //Create Student
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            Guid guid = Guid.NewGuid();
            string userId = student.StudentName + guid.ToString("N").Substring(0, 6);
            student.UserId = userId;
            
            context.Students.Add(student);
            context.SaveChanges();

            User user = new User();
            user.ContactNo = student.Contact;
            user.Email = student.Email;
            user.RoleId = 3;
            user.Password = student.UserId;
            user.UserId = student.UserId;
            context.Users.Add(user);
            context.SaveChanges();

            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            var batches = context.Batches.ToList();
            ViewBag.Departments = departments;
            ViewBag.Batches = batches;

            var departmentName = context.Departments
    .Where(department => department.DepartmentId == student.DepartmentId)
    .Select(department => department.DepartmentName)
    .FirstOrDefault();

            var batchName = context.Batches
    .Where(batch => batch.BatchId == student.BatchId)
    .Select(batch => batch.BatchName)
    .FirstOrDefault();
            var studentname=student.StudentName;
            var message = $"<h1>Dear {studentname},</h1><br /><br />"
    + $"<p>Thank you for registering as a student.</p>"
    + $"<p>Your details:</p>"
    + $"<ul>"
    + $"<li>User ID: {user.UserId}</li>"
    + $"<li>Password: {user.Password}</li>"
    + $"<li>Department: {departmentName}</li>"
    + $"<li>Batch: {batchName}</li>"
    + $"</ul>"
    + "<p>Best regards,<br />Uits</p>";

            SendEmailAsync(user.Email, "Student registration", message);

            return RedirectToAction("GetAllStudent", "Student");
        }


        //Update or Edit Student
        public IActionResult UpdateStudentDetails(int id)
        {
            Student aStudent = context.Students.FirstOrDefault(x => x.StudentId == id);
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            var batches = context.Batches.Where(x => x.DepartmentId == aStudent.DepartmentId).ToList();
            ViewBag.Departments = departments;
            ViewBag.Batches = batches;
            return View(aStudent);
        }

        [HttpPost]
        public IActionResult UpdateStudentDetails(int id, Student student)
        {
            var aStudent = context.Students.Find(id);

            aStudent.Sex = student.Sex;
            //aStudent.StudentId=student.StudentId;
            aStudent.StudentName = student.StudentName;
            aStudent.DepartmentId = student.DepartmentId;
            aStudent.BatchId = student.BatchId;
            //aStudent.StudentPassword=student.StudentPassword;
            aStudent.Contact = student.Contact;
            aStudent.Sex = student.Sex;
            aStudent.Email = student.Email;
            aStudent.Address = student.Address;
            context.Students.Update(aStudent);
            context.SaveChanges();

            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            var batches = context.Batches.ToList();
            ViewBag.Departments = departments;
            ViewBag.Batches = batches;
            return RedirectToAction("GetAllStudent", "Student");
        }

        //Delete
        public IActionResult DeleteStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteStudent(int newId)
        {
            var aStudent = context.Students.Find(newId);
            context.Students.Remove(aStudent);
            context.SaveChanges();
            return RedirectToAction("GetAllStudent", "Student");
        }

        public JsonResult GetDepartment()
        {
            var cnt = context.Departments.ToList();
            return new JsonResult(cnt);
        }
        public JsonResult GetBatch(int departmentId)
        {
            var data = context.Batches.Where(x => x.DepartmentId == departmentId).ToList();
            return new JsonResult(data);
        }

    }
}
