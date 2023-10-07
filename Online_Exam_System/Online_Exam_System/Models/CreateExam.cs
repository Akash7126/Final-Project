using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class CreateExam
    {
        public CreateExam()
        {
            Questions = new HashSet<Question>();
        }

        public int ExamId { get; set; }
        public string? ExamTitle { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
