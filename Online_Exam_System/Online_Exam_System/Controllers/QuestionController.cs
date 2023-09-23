using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam_System.Models;
using Online_Exam_System.ViewModel;
using System.Web;

namespace Online_Exam_System.Controllers
{
    public class QuestionController : Controller
    {
        private readonly OnlineExamSystemContext _context;
        public QuestionController(OnlineExamSystemContext context)
        {
            _context = context;
        }
        //public IActionResult GetCourseCourseAssign()
        //{
        //    var courseAssigns = (from courseAssign in _context.CourseAssigns
        //                         join course in _context.Courses
        //                         on courseAssign.CourseId equals course.CourseId
        //                         select new { course.CourseId, course.CourseCode }).Distinct().ToList();
        //    ViewBag.CourseAssigns = courseAssigns;
        //    return View();  
        //}
        public IActionResult QuestionBank()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddQestion()
        {
            var courseAssigns = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            
            ViewBag.CourseAssigns = courseAssigns;
            return View();
        }

        [HttpPost]
        public IActionResult AddQestion(QuestionAnsweViewModel question)
        {
           
           // answerList.Add(question.editordata1);

            var questionEntity = new Question();
            questionEntity.QuestionDescription = question.QuestionDescription;



            var courseAssigns = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns = courseAssigns;
            _context.Questions.Add(questionEntity);
            _context.SaveChanges();

            var answerList = new List<Answer>();

            var answer1 = new Answer();
            answer1.AnswerText = question.editordata1;

            if (question.editordata1Answer != null)
            {
                answer1.IsCorrect = true;
            }
            else
            {
                answer1.IsCorrect = false;
            }
            answer1.QuestionId = questionEntity.QuestionId;

            var answer7 = new Answer();
            answer7.AnswerText = question.editordata7;

            if (question.editordata7Answer != null)
            {
                answer7.IsCorrect = true;
            }
            else
            {
                answer7.IsCorrect = false;
            }
            answer7.QuestionId = questionEntity.QuestionId;

            var answer2 = new Answer();
            answer2.AnswerText = question.editordata2;

            if (question.editordata2Answer != null)
            {
                answer2.IsCorrect = true;
            }
            else
            {
                answer2.IsCorrect = false;
            }

            answer2.QuestionId = questionEntity.QuestionId;

            var answer3 = new Answer();
            answer3.AnswerText = question.editordata3;
            if (question.editordata3Answer != null)
            {
                answer3.IsCorrect = true;
            }
            else
            {
                answer3.IsCorrect = false;
            }
            answer3.QuestionId = questionEntity.QuestionId;

            answerList.Add(answer1);
            answerList.Add(answer7);
            answerList.Add(answer2);
            answerList.Add(answer3);

            _context.Answers.AddRange(answerList);
            _context.SaveChanges();
           // var answerList = new List<Answer>
           // {
           //   new Answer()
           //     {
           //         AnswerText = question.editordata1,
           //         IsCorrect = true,
           //         QuestionId = questionEntity.QuestionId,
           //     }
           //};
           

            return View();
        }


        public IActionResult AddMultipleSelectQuestion()
        {
            var courseAssigns1 = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns1 = courseAssigns1;
            return View();
            
        }

        public IActionResult AddTrueFalseQuestion()
        {
            var courseAssigns2 = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns2 = courseAssigns2;
            return View();
        }

       // public IActionResult AddQuestionDescription()
       // {
       //     return View();
       // }

       //[HttpPost]
       // public IActionResult AddQuestionDescription(QuestionAnsweViewModel question)
       // {
       //     //_context.Questions.Add(question);
       //     //_context.SaveChanges();
       //     return View();  
       // }
    }
}
