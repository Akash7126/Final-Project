using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class CreateWrittenExam
    {
        public int ExamId { get; set; }
        public string? ExamTitle { get; set; }
        public string? Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? TeacherId { get; set; }
        public int? CourseId { get; set; }
    }
}
