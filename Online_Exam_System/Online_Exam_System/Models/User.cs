using System;
using System.Collections.Generic;

namespace Online_Exam_System.Model
{
    public partial class User
    { 
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
    }
}
