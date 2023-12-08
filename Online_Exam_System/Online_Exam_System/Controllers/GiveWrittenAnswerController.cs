using Microsoft.AspNetCore.Mvc;
using Online_Exam_System.Models;

namespace Online_Exam_System.Controllers
{
    public class GiveWrittenAnswerController : Controller
    {
        private readonly OnlineExamSystemContext _context;

        public GiveWrittenAnswerController(OnlineExamSystemContext context)
        {
            _context = context;
        }


        public IActionResult UploadPdf(GivenWrittenAnswer givenWrittenAnswer)
        {
            if (givenWrittenAnswer.ProfilePic != null && givenWrittenAnswer.ProfilePic.Length > 0)
            {
                // Save the file to a folder on the server (you may want to customize the path)
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(givenWrittenAnswer.ProfilePic.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Pdf", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    givenWrittenAnswer.ProfilePic.CopyTo(fileStream);
                }

                // Save the file path to the database
                givenWrittenAnswer.QuestionPdf = "/images/Pdf/" + fileName;


                using (var dbContext = new OnlineExamSystemContext())
                {
                    // Replace YourEntity with the actual entity type you are using to store this information
                    GivenWrittenAnswer entity = new GivenWrittenAnswer
                    {
                        StudentId = givenWrittenAnswer.StudentId,
                        TeacherId = givenWrittenAnswer.TeacherId,
                        ExamId = givenWrittenAnswer.ExamId,
                        // Add other properties as needed
                        QuestionPdf = givenWrittenAnswer.QuestionPdf
                    };

                    dbContext.GivenWrittenAnswers.Add(entity);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index","Home");
        }





       


       

    }
}
