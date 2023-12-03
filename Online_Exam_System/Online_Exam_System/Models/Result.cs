using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class Result
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }
        public int? TottalMarks { get; set; }
        public int? StudentAnswerId { get; set; }
        public int? ExamId { get; set; }
        public int? QuestionId { get; set; }
    }
}
