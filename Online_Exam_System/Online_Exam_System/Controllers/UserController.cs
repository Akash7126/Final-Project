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
         
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (model.UserId != null && model.Password != null)
            {
                var user = _context.Users.Where(x => x.UserId == model.UserId && x.Password == model.Password).ToList();
                if (user.Count > 0)
                {
                    //get role name from user
                    var userId = model.UserId;
                    var roleName = _context.Users.Include(x => x.Role).Where(x => x.UserId == userId).Select(x => x.Role.RoleName).FirstOrDefault();
                    var roleId = _context.Users.Include(x => x.Role).Where(x => x.UserId == userId).Select(x => x.Role.Id).FirstOrDefault();
                    //set RoleName into session
                    HttpContext.Response.Cookies.Append("RoleName", roleName);
                    HttpContext.Response.Cookies.Append("RoleId", roleId.ToString());
                    HttpContext.Response.Cookies.Append("UserId", userId.ToString());

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
                else
                {
                    ViewBag.ErrorMessage = "Username or password is incorrect.";
                    return View();
                }

            }
            //Get Role Name from Use Table
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the user based on the logged-in user's ID or any other identifier.
                var userId = HttpContext.Request.Cookies["UserId"];
                // Implement this method to get the user ID.
                var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

                if (user != null && user.Password == model.CurrentPassword)
                {
                    // Update the user's password with the new password.
                    user.Password = model.NewPassword;
                    _context.SaveChanges();
                    ViewBag.Message = "Password changed successfully!";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid current password");
                }
            }

            return View(model);
        }

    }
}
