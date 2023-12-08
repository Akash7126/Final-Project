using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class StudentWrittenQuestion
    {
        public int WrittenQuestionId { get; set; }
        public string? QuestionDescription { get; set; }
        public decimal? Mark { get; set; }
        public int? CourseId { get; set; }
        public int? TeacherId { get; set; }
        public int? ExamId { get; set; }
        public int? QuestionTypeId { get; set; }
    }
}
