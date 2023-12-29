using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using Online_Exam_System.Models;
using Online_Exam_System.ViewModel;
using System.Diagnostics;

namespace Online_Exam_System.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}


        private readonly ILogger<HomeController> _logger;
        private readonly OnlineExamSystemContext _context; // Assuming YourDbContext is your DbContext class

        public HomeController(ILogger<HomeController> logger, OnlineExamSystemContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            var userId = HttpContext.Request.Cookies["UserId"];

            var users = _context.Users.Where(u => u.UserId == userId).FirstOrDefault();

            if (users.RoleId == 2)
            {

                var query = from user in _context.Users
                            join teacher in _context.Teachers on user.UserId equals teacher.UserId
                            join department in _context.Departments on teacher.DepartmentId equals department.DepartmentId
                            where user.UserId == users.UserId
                            select new IndexViewModel
                            {
                              UserId =  teacher.UserId,
                              TeacherName =  teacher.TeacherName,
                              DepartmentId =  teacher.DepartmentId,
                              DepartmentName =  department.DepartmentName,
                              JobTitle =  teacher.JobTitle,
                              Contact =  teacher.Contact,
                              Email =  teacher.Email,
                              ProfilePicPath =  teacher.ProfilePicPath,
                              RoleId = user.RoleId
                            };

                return View(query.FirstOrDefault());


            }
            else if (users.RoleId == 3)
            {
                var query = from user in _context.Users
                            join student in _context.Students on user.UserId equals student.UserId
                            join department in _context.Departments on student.DepartmentId equals department.DepartmentId
                            where user.UserId == users.UserId
                            select new IndexViewModel
                            {
                                UserId = student.UserId,
                                StudentName = student.StudentName,
                                DepartmentId = student.DepartmentId,
                                DepartmentName = department.DepartmentName,
                                
                                Contact = student.Contact,
                                Email = student.Email,
                                ProfilePicPath = student.ProfilePicPath,
                                RoleId = user.RoleId
                            };

                return View(query.FirstOrDefault());

            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}