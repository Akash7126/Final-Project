using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Exam_System.Models
{
    public partial class GivenWrittenAnswer
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }
        public int? ExamId { get; set; }
        [NotMapped]
        public IFormFile ProfilePic { get; set; }
        public string? QuestionPdf { get; set; }
    }
}
