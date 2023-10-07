using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class RoleWiseMenuPermission
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? MenuId { get; set; }
    }
}
