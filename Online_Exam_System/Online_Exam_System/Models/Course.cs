using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class Course
    {
        public Course()
        {
            StudentRegistrations = new HashSet<StudentRegistration>();
        }

        public int CourseId { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseTittle { get; set; }
        public double? CourseCredit { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<StudentRegistration> StudentRegistrations { get; set; }
    }
}
