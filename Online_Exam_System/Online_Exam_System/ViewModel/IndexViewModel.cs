using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Exam_System.ViewModel
{
    public class IndexViewModel
    {
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? TeacherPassword { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? JobTitle { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Sex { get; set; }
        public string? UserId { get; set; }
        public int? RoleId { get; set; }

        [NotMapped]
        public IFormFile ProfilePic { get; set; }
        public string? ProfilePicPath { get; set; }

        public string? StudentName { get; set; }

        public virtual Department Department { get; set; } = null!;


    }
}
