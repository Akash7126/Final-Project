using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Online_Exam_System.Models;
using Online_Exam_System.ViewModel;
using System;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StudentAnswerController : Controller
{
    private readonly OnlineExamSystemContext _context;

    public StudentAnswerController(OnlineExamSystemContext context)
    {
        _context = context;
    }
    private IEnumerable<int> GetAllAnswerIdsForQuestion(int questionId)
    {
        // Retrieve all answer IDs for a given question from your database
        return _context.Answers
            .Where(a => a.QuestionId == questionId)
            .Select(a => a.AnswerId)
            .ToList();
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


    [HttpPost]
    public IActionResult GiveStudentAnswer(Dictionary<int, int[]> selectedAnswerIds, Dictionary<int, int> questionIds, int totalmark, int totalquestion, int examId,int teacherId)
    {
        var userId = HttpContext.Request.Cookies["UserId"];
        var student = _context.Students.FirstOrDefault(s => s.UserId == userId);

        var studentId = student?.StudentId;

        if (studentId.HasValue)
        {

            var studentAnswer = new StudentAnswer();
            // Insert student answers into StudentAnswer table
            foreach (var (questionId, answerIds) in selectedAnswerIds)
            {
                var _context = new OnlineExamSystemContext();

                var studentAnswerList = new List<StudentAnswer>();

                foreach (var answerId in answerIds)
                {
                    studentAnswer = new StudentAnswer
                    {
                        StudentId = studentId.Value, 
                        QuestionId = questionId,
                        AnswerId = answerId,
                        Examid = examId
                        // Additional properties can be set here
                    };

                    studentAnswerList.Add(studentAnswer);

                    _context.StudentAnswers.Add(studentAnswer);
                }
                var answerList = _context.Answers.ToList().Where(x => x.QuestionId == questionId && x.IsCorrect == true).ToList();
                bool result = CompareLists(studentAnswerList, answerList);

                if (result == true)
                {
                    _context.SaveChanges();

                }

            }
        }

        using (var _context = new OnlineExamSystemContext())
        {
            foreach (var (questionId, answerIds) in selectedAnswerIds)
            {
                var allAnswerIds = GetAllAnswerIdsForQuestion(questionId);

                var studentGivenAnswerList = allAnswerIds.Select(answerId => new StudentGivenAnswer
                {
                    StudentId = studentId.Value,
                    QuestionId = questionId,
                    AnswerId = answerId,
                    IsSelect = answerIds.Contains(answerId),
                    // Additional properties can be set here
                }).ToList();

                _context.StudentGivenAnswers.AddRange(studentGivenAnswerList);
            }

            _context.SaveChanges();
        }


        var Marks = _context.StudentAnswers
    .Where(sa => sa.Examid == examId && sa.StudentId == studentId) // Replace with the desired ExamId
    .Join(
        _context.Questions,
        sa => sa.QuestionId,
        q => q.QuestionId,
        (sa, q) => new { sa, q }
    )
    .Select(x => new { x.sa.Examid, x.q.QuestionId, x.q.Mark })
    .Distinct() // Ensure distinct QuestionId values
    .GroupBy(x => new { x.Examid })
    .Select(group => new
    {
        Examid = group.Key.Examid,
        OverallTotalMarks = group.Sum(x => x.Mark)
    })
    .Sum(x => x.OverallTotalMarks);

        using (var context = new OnlineExamSystemContext())
        {
            // Calculate total marks for the student (adjust the logic based on your requirements)
            var totalMarks = Marks;

            // Insert into Result table
            context.Results.Add(new Result
            {
                StudentId = studentId,
                TeacherId = teacherId, // Assuming TeacherId is related to the exam
                TottalMarks = Convert.ToInt32(totalMarks),
                ExamId = examId
                //QuestionId=
            });

            context.SaveChanges();
        }


var overallTotalMarks = _context.StudentAnswers
    .Where(sa => sa.Examid == examId && sa.StudentId == studentId) // Replace with the desired ExamId
    .Join(
        _context.Questions,
        sa => sa.QuestionId,
        q => q.QuestionId,
        (sa, q) => new { sa, q }
    )
    .Select(x => new { x.sa.Examid, x.q.QuestionId, x.q.Mark })
    .Distinct() // Ensure distinct QuestionId values
    .GroupBy(x => new { x.Examid })
    .Select(group => new
    {
        Examid = group.Key.Examid,
        OverallTotalMarks = group.Sum(x => x.Mark)
    })
    .Sum(x => x.OverallTotalMarks);

        var numberOfAnsweredQuestions = _context.StudentAnswers
    .Where(sa => sa.Examid == examId && sa.StudentId == studentId) // Replace 31 with the desired ExamId
    .Select(sa => sa.QuestionId)
    .Distinct()
    .Count();

        var QuestionTotalMarks = totalmark;

        var OverallTotalMarks = overallTotalMarks;

        double percentage = (double)overallTotalMarks / QuestionTotalMarks * 100;

        

        var studentname = student.StudentName;
        var studentEmail = student.Email;
        var message = $"<h1>Dear {studentname},</h1><br /><br />"
+ $"<p>Thank you {studentname}.</p>"
+ $"<p>Your Result:</p>"
+ $"<ul>"
+ $"<li>Your given Correct Anser is: {numberOfAnsweredQuestions}</li>"
+ $"<li>your mark is: {OverallTotalMarks}</li>"
+ $"<li>Percentage is: {percentage}</li>"
+ $"</ul>"
+ "<p>Best regards,<br />Uits</p>";

SendEmailAsync(studentEmail, "Student Result", message);




        var viewModel = new QuizResultViewModel
        {
            ExamId = examId,
            TotalMarks = QuestionTotalMarks,
            NumberOfAnsweredQuestions =Convert.ToInt32( numberOfAnsweredQuestions),
            OverallTotalMarks = Convert.ToInt32( overallTotalMarks),
            OverallTotalQuestions = totalquestion,
            Percentage = percentage
        };
        TempData["QuizResultViewModel"] = viewModel;

        return View("GiveStudentAnswer", viewModel);
    }


    [HttpPost]
    public IActionResult CheckAnswers(int examId)
    {
        var userId = HttpContext.Request.Cookies["UserId"];
        var student = _context.Students.FirstOrDefault(s => s.UserId == userId);

        var studentId = student?.StudentId;

        var studentGivenAnswersQuery = (from sga in _context.StudentGivenAnswers
                                        join q in _context.Questions on sga.QuestionId equals q.QuestionId
                                        join a in _context.Answers on sga.AnswerId equals a.AnswerId
                                        where sga.StudentId == studentId
                                        group new { a.AnswerId, a.AnswerText, sga.IsSelect }
        by new { q.QuestionId, q.QuestionDescription, q.QuestionTypeId, sga.StudentId } into g
                                        select new StudentGivenAnswersViewModel
                                        {
                                            QuestionId = g.Key.QuestionId,
                                            QuestionDescription = g.Key.QuestionDescription,
                                            QuestionTypeId = g.Key.QuestionTypeId,
                                            StudentId = g.Key.StudentId,
                                            Answers = g.Select(a => new Answer
                                            {
                                                AnswerId = a.AnswerId,
                                                AnswerText = a.AnswerText,
                                                IsCorrect = a.IsSelect
                                            }).ToList()
                                        });

        var distinctStudentGivenAnswers = studentGivenAnswersQuery.ToList();


        var exam = _context.CreateExams
                     .FirstOrDefault(e => e.ExamId == examId);


        var distinctExams = (from ca in _context.CourseAssigns
                             join q in _context.Questions on ca.CourseId equals q.CourseId
                             join ce in _context.CreateExams on q.ExamId equals ce.ExamId
                             join t in _context.Students on ca.StudentId equals t.StudentId
                             join c in _context.Courses on q.CourseId equals c.CourseId
                             join d in _context.Departments on c.DepartmentId equals d.DepartmentId
                             where t.UserId == userId && ce.ExamId == examId
                             select new DisplayStudentExamViewModel
                             {
                                 ExamId = ce.ExamId,
                                 ExamTitle = ce.ExamTitle,
                                 StartTime = ce.StartTime,
                                 EndTime = ce.EndTime,
                                 CourseTittle = c.CourseTittle,
                                 CourseCode = c.CourseCode,
                                 DepartmentName = d.DepartmentName
                             }).Distinct().ToList();


        var totalMarks = _context.Questions
                .Where(q => q.ExamId == examId)
                .Sum(q => q.Mark);

        foreach (var distinctExam in distinctExams)
        {
            var time = distinctExam.EndTime - distinctExam.StartTime;

            int totalMinutes = (int)time.TotalMinutes;
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;

            string formattedTime;

            if (hours > 0)
            {
                formattedTime = $"{hours}hr {minutes}min";
            }
            else
            {
                formattedTime = $"{minutes}min";
            }

            ViewBag.Time = formattedTime;
        }

        ViewBag.ExamEndTime = exam.EndTime;
        ViewBag.DistinctExams = distinctExams;
        ViewBag.TotalMarks = totalMarks;


        return View(distinctStudentGivenAnswers);
    }





    
    public IActionResult StudentCheckAnswers(int examId,int Studentid)
    {


        var studentId = Studentid;

        var studentGivenAnswersQuery = (from sga in _context.StudentGivenAnswers
                                        join q in _context.Questions on sga.QuestionId equals q.QuestionId
                                        join a in _context.Answers on sga.AnswerId equals a.AnswerId
                                        where sga.StudentId == studentId
                                        group new { a.AnswerId, a.AnswerText, sga.IsSelect }
        by new { q.QuestionId, q.QuestionDescription, q.QuestionTypeId, sga.StudentId } into g
                                        select new StudentGivenAnswersViewModel
                                        {
                                            QuestionId = g.Key.QuestionId,
                                            QuestionDescription = g.Key.QuestionDescription,
                                            QuestionTypeId = g.Key.QuestionTypeId,
                                            StudentId = g.Key.StudentId,
                                            Answers = g.Select(a => new Answer
                                            {
                                                AnswerId = a.AnswerId,
                                                AnswerText = a.AnswerText,
                                                IsCorrect = a.IsSelect
                                            }).ToList()
                                        });

        var distinctStudentGivenAnswers = studentGivenAnswersQuery.ToList();


        var exam = _context.CreateExams
                     .FirstOrDefault(e => e.ExamId == examId);


        var distinctExams = (from ca in _context.CourseAssigns
                             join q in _context.Questions on ca.CourseId equals q.CourseId
                             join ce in _context.CreateExams on q.ExamId equals ce.ExamId
                             join t in _context.Students on ca.StudentId equals t.StudentId
                             join c in _context.Courses on q.CourseId equals c.CourseId
                             join d in _context.Departments on c.DepartmentId equals d.DepartmentId
                             where t.StudentId == studentId && ce.ExamId == examId
                             select new DisplayStudentExamViewModel
                             {
                                 ExamId = ce.ExamId,
                                 ExamTitle = ce.ExamTitle,
                                 StartTime = ce.StartTime,
                                 EndTime = ce.EndTime,
                                 CourseTittle = c.CourseTittle,
                                 CourseCode = c.CourseCode,
                                 DepartmentName = d.DepartmentName
                             }).Distinct().ToList();


        var totalMarks = _context.Questions
                .Where(q => q.ExamId == examId)
                .Sum(q => q.Mark);

        foreach (var distinctExam in distinctExams)
        {
            var time = distinctExam.EndTime - distinctExam.StartTime;

            int totalMinutes = (int)time.TotalMinutes;
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;

            string formattedTime;

            if (hours > 0)
            {
                formattedTime = $"{hours}hr {minutes}min";
            }
            else
            {
                formattedTime = $"{minutes}min";
            }

            ViewBag.Time = formattedTime;
        }

        ViewBag.ExamEndTime = exam.EndTime;
        ViewBag.DistinctExams = distinctExams;
        ViewBag.TotalMarks = totalMarks;


        return View(distinctStudentGivenAnswers);
    }




    static bool CompareLists(List<StudentAnswer> studentAnswerList, List<Answer> answerList)
    {
        return studentAnswerList.All(sa =>
            answerList.Any(a =>
                a.QuestionId == sa.QuestionId && a.AnswerId == sa.AnswerId
            )
        );
    }





}
