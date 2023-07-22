using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class Batch
    {
        public Batch()
        {
            Students = new HashSet<Student>();
        }

        public int BatchId { get; set; }
        public string? BatchName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
