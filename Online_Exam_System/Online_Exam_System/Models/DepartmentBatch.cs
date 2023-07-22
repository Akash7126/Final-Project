using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class DepartmentBatch
    {
        public int DepartmentId { get; set; }
        public int BatchId { get; set; }

        public virtual Batch Batch { get; set; } = null!;
        public virtual Department Department { get; set; } = null!;
    }
}
