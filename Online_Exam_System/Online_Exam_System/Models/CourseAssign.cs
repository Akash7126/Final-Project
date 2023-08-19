using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public List<int> StudentList { get; set; }
        public virtual Department IdNavigation { get; set; } = null!;
    }
}
