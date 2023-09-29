using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.ViewModel;

namespace Online_Exam_System.Controllers
{
    public class UserController : Controller
    {
        private readonly OnlineExamSystemContext _context;

        public UserController(OnlineExamSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AppStartView()
        {
            return View();
        }
         
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.UserId != null && model.Password != null)
            {
                //get role name from user
                var userId = model.UserId;
                var roleName = _context.Users.Include(x => x.Role).Where(x => x.UserId == userId).Select(x => x.Role.RoleName).FirstOrDefault();
                var roleId = _context.Users.Include(x => x.Role).Where(x => x.UserId == userId).Select(x => x.Role.Id).FirstOrDefault();
                //set RoleName into session
                HttpContext.Response.Cookies.Append("RoleName", roleName);
                HttpContext.Response.Cookies.Append("RoleId", roleId.ToString());

                if (roleName == "Admin")
                {
                  //  ViewBag.Role = "Admin";
                    return RedirectToAction("Index", "Home");
                }
                else if (roleName == "Teacher")
                {
                    // ViewBag.Role = "Teacher";
                    return RedirectToAction("Index", "Home");
                }
                else if (roleName == "Student")
                {
                    // ViewBag.Role = "Student";
                    return RedirectToAction("Index", "Home");
                }
            }
                //Get Role Name from Use Table
                return View();
        }
    }
}
