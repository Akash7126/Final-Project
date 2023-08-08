using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class Teacher
    {
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? TeacherPassword { get; set; }
        public int DepartmentId { get; set; }
        public string? JobTitle { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Sex { get; set; }
        public virtual Department Department { get; set; } = null!;
    }
}
