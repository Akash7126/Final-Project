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
            var courseAssingCourseid = _context.CourseAssigns.FirstOrDefault(s => s.CourseId == question.CourseId);
            var  teacherId = courseAssingCourseid.TeacherId;

           // answerList.Add(question.editordata1);
            var questionEntity = new Question();
            questionEntity.QuestionDescription = question.QuestionDescription;

            questionEntity.CourseId= courseAssingCourseid.CourseId;
            questionEntity.TeacherId= teacherId;

            var courseAssigns = (from courseAssign in _context.CourseAssigns
                                 join course in _context.Courses
                                 on courseAssign.CourseId equals course.CourseId
                                 select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns = courseAssigns;
            _context.Questions.Add(questionEntity);
            _context.SaveChanges();

            var answerList = new List<Answer>();

            var answer1 = new Answer();
            answer1.AnswerText = question.Question1;

            if (question.Question1Answer != null)
            {
                answer1.IsCorrect = true;
            }
            else
            {
                answer1.IsCorrect = false;
            }
            answer1.QuestionId = questionEntity.QuestionId;

            var answer7 = new Answer();
            answer7.AnswerText = question.Question7;

            if (question.Question7Answer != null)
            {
                answer7.IsCorrect = true;
            }
            else
            {
                answer7.IsCorrect = false;
            }
            answer7.QuestionId = questionEntity.QuestionId;

            var answer2 = new Answer();
            answer2.AnswerText = question.Question2;

            if (question.Question2Answer != null)
            {
                answer2.IsCorrect = true;
            }
            else
            {
                answer2.IsCorrect = false;
            }

            answer2.QuestionId = questionEntity.QuestionId;

            var answer3 = new Answer();
            answer3.AnswerText = question.Question3;
            if (question.Question3Answer != null)
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
        [HttpPost]
        public IActionResult AddMultipleSelectQuestion(QuestionAnsweViewModel question)
        {
            var courseAssingCourseid1 = _context.CourseAssigns.FirstOrDefault(s => s.CourseId == question.CourseId);
            var teacherId = courseAssingCourseid1.TeacherId;

            // answerList.Add(question.editordata1);
            var questionEntity1 = new Question();
            questionEntity1.QuestionDescription = question.QuestionDescription;

            questionEntity1.CourseId = courseAssingCourseid1.CourseId;
            questionEntity1.TeacherId = teacherId;

            var courseAssigns1 = (from courseAssign in _context.CourseAssigns
                                  join course in _context.Courses
                                  on courseAssign.CourseId equals course.CourseId
                                  select new { course.CourseId, course.CourseCode }).Distinct().ToList();
            ViewBag.CourseAssigns1 = courseAssigns1;
            _context.Questions.Add(questionEntity1);
            _context.SaveChanges();


            var answerList = new List<Answer>();
            var answer1 = new Answer();
            answer1.AnswerText = question.Question1;
            ////////////////////////////////////////////////////////
            if (!string.IsNullOrEmpty(question.Question1Answer))
            {
                answer1.IsCorrect = true;
            }
            else
            {
                answer1.IsCorrect = false;
            }
            answer1.QuestionId = questionEntity1.QuestionId;

            var answer7 = new Answer();
            answer7.AnswerText = question.Question7;

            if (!string.IsNullOrEmpty(question.Question7Answer))
            {
                answer7.IsCorrect = true;
            }
            else
            {
                answer7.IsCorrect = false;
            }
            answer7.QuestionId = questionEntity1.QuestionId;

            var answer2 = new Answer();
            answer2.AnswerText = question.Question2;

            if (!string.IsNullOrEmpty(question.Question2Answer))
            {
                answer2.IsCorrect = true;
            }
            else
            {
                answer2.IsCorrect = false;
            }

            answer2.QuestionId = questionEntity1.QuestionId;

            var answer3 = new Answer();
            answer3.AnswerText = question.Question3;
            if (!string.IsNullOrEmpty(question.Question3Answer))
            {
                answer3.IsCorrect = true;
            }
            else
            {
                answer3.IsCorrect = false;
            }
            answer3.QuestionId = questionEntity1.QuestionId;

            answerList.Add(answer1);
            answerList.Add(answer7);
            answerList.Add(answer2);
            answerList.Add(answer3);

            _context.Answers.AddRange(answerList);
            _context.SaveChanges();
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
    }
}
