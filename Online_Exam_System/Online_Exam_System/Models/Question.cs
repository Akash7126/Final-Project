using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int QuestionId { get; set; }
        public string? QuestionDescription { get; set; }
        public int? QuestionTypeId { get; set; }
        public decimal? Mark { get; set; }
        public int? QuestionOrder { get; set; }
        public int? CourseId { get; set; }
        public int? TeacherId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
