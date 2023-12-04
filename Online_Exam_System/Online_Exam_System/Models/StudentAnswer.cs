using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class StudentAnswer
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int? StudentId { get; set; }
        public int? Examid { get; set; }
    }
}
