using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class CourseAssign
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public int BatchId { get; set; }
        public int StudentId { get; set; }

        public virtual Department Department { get; set; } = null!;
    }
}
