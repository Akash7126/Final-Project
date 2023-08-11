using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var data = context.Students.Include(x=>x.Department).Include(x =>x.Batch).ToList(); ///lambda expression
            return View(data);
        }

        //get Student Details//1
        public IActionResult GetStudentDetails(int id)
        {
            var studentById = context.Students.Include(x => x.Department).Include(x => x.Batch).FirstOrDefault(s => s.StudentId == id); ;
            return View(studentById);
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
            context.Students.Add(student);
            context.SaveChanges();

            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            var batches = context.Batches.ToList();
            ViewBag.Departments = departments;
            ViewBag.Batches = batches;

            return RedirectToAction("GetAllStudent", "Student");
        }


        //Update or Edit Student
        public IActionResult UpdateStudentDetails(int id)
        {
            Student aStudent=context.Students.FirstOrDefault(x=>x.StudentId == id);
            var departments = context.Departments.ToList(); // Replace this with the method that retrieves the departments from your data source.
            var batches = context.Batches.Where(x => x.DepartmentId == aStudent.DepartmentId).ToList();
            ViewBag.Departments = departments;
            ViewBag.Batches = batches;
            return View(aStudent);
        }

        [HttpPost]
        public IActionResult UpdateStudentDetails(int id,Student student)
        {
            var aStudent = context.Students.Find(id);

            aStudent.Sex = student.Sex;
            //aStudent.StudentId=student.StudentId;
            aStudent.StudentName=student.StudentName;
            aStudent.DepartmentId=student.DepartmentId;
            aStudent.BatchId=student.BatchId;
            aStudent.StudentPassword=student.StudentPassword;
            aStudent.Contact=student.Contact;
            aStudent.Sex=student.Sex;
            aStudent.Email=student.Email;
            aStudent.Address=student.Address;
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
