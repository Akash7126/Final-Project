using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Exam_System.ViewModel
{
    public class CourseAssignViewModel
    {
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public int BatchId { get; set; }
        public List<int> StudentList { get; set; }

    }
}
