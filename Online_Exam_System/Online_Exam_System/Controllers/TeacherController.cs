using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Online_Exam_System.Models;

namespace Online_Exam_System.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ILogger<TeacherController> _logger;
        private readonly OnlineExamSystemContext context;

        public TeacherController(ILogger<TeacherController> logger, OnlineExamSystemContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult GetAllTeacher()
        {
            var data = context.Teachers.Include(x => x.Department).ToList(); ///lambda expression
            return View(data);
        }

        //get Teacher Details//1
        public IActionResult GetTeacherDetails(int id)
        {
            var TeacherById = context.Teachers.Include(x => x.Department).FirstOrDefault(s => s.TeacherId == id); ;
            return View(TeacherById);
        }

        //create Teacher
        public IActionResult AddTeacher()
        {
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;
            return View();
        }
        //Create Teacher
        [HttpPost]
        public IActionResult AddTeacher(Teacher teacher)
        {
            //new code for create userId
            Guid guid = Guid.NewGuid();
            string userId = teacher.TeacherName + guid.ToString("N").Substring(0, 6);
            teacher.UserId = userId;

            context.Teachers.Add(teacher);
            context.SaveChanges();

            //add into usertable
            User user = new User();
            user.ContactNo = teacher.Contact;
            user.Email = teacher.Email;
            user.RoleId = 2;
            user.Password = teacher.UserId;
            user.UserId = teacher.UserId;
            context.Users.Add(user);
            context.SaveChanges();


            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;
            return RedirectToAction("GetAllTeacher", "Teacher");
        }


        //Update or Edit Student
        public IActionResult UpdateTeacherDetails(int id)
        {
            Teacher Ateacher = context.Teachers.FirstOrDefault(x => x.TeacherId == id);
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;
            return View(Ateacher);
        }

        [HttpPost]
        public IActionResult UpdateTeacherDetails(int id, Teacher teacher)
        {
            var aTeacher = context.Teachers.Find(id);

            aTeacher.Sex = teacher.Sex;
            //aTeacher.StudentId=student.StudentId;
            aTeacher.TeacherName = teacher.TeacherName;
            aTeacher.DepartmentId = teacher.DepartmentId;
            aTeacher.TeacherPassword = teacher.TeacherPassword;
            aTeacher.Contact = teacher.Contact;
            aTeacher.JobTitle = teacher.JobTitle;
            aTeacher.Sex = teacher.Sex;
            aTeacher.Email = teacher.Email;
            aTeacher.Address = teacher.Address;
            context.Teachers.Update(aTeacher);
            context.SaveChanges();

            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            ViewBag.Departments = departments;
            return RedirectToAction("GetAllTeacher", "Teacher");
        }

        //Delete
        public IActionResult DeleteTeacher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteTeacher(int newId)
        {
            var aTeacher = context.Teachers.Find(newId);
            context.Teachers.Remove(aTeacher);
            context.SaveChanges();
            return RedirectToAction("GetAllTeacher", "Teacher");
        }
    }
}
