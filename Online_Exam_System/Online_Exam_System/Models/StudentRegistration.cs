using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class StudentRegistration
    {
        public int StudentRegistrationId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
