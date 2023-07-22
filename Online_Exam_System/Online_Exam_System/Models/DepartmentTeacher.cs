using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class DepartmentTeacher
    {
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
