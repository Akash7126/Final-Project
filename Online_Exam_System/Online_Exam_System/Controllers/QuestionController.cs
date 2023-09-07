using Microsoft.AspNetCore.Mvc;

namespace Online_Exam_System.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult AddQestion()
        {
            return View();
        }

        public IActionResult AddMultipleSelectQuestion()
        {
            return View();
        }

        public IActionResult AddTrueFalseQuestion()
        {
            return View();
        }
    }
}
