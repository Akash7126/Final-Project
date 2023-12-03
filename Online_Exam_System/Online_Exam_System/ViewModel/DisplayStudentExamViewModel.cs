namespace Online_Exam_System.ViewModel
{
    public class DisplayStudentExamViewModel
    {
        public int ExamId { get; set; }
        public string? ExamTitle { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CourseId { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseTittle { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
