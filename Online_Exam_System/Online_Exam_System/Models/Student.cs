﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Exam_System.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentRegistrations = new HashSet<StudentRegistration>();
        }

        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public int DepartmentId { get; set; }
        public int BatchId { get; set; }
        public string? StudentPassword { get; set; }
        public string? Contact { get; set; }
        public string? Sex { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? UserId { get; set; }

        [NotMapped]
        public IFormFile ProfilePic { get; set; }
        public string? ProfilePicPath { get; set; }

        public virtual Batch Batch { get; set; } = null!;
        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<StudentRegistration> StudentRegistrations { get; set; }
    }
}
