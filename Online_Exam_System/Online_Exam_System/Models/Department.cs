using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class Department
    {
        public Department()
        {
            Batches = new HashSet<Batch>();
            CourseAssigns = new HashSet<CourseAssign>();
            Courses = new HashSet<Course>();
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }

        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<CourseAssign> CourseAssigns { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
