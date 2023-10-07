namespace Online_Exam_System.ViewModel
{
    public class CreateExamViewModel
    {
        public CreateExamViewModel()
        {
            Questions = new HashSet<Question>();
        }

        public int ExamId { get; set; }
        public string? ExamTitle { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int? CourseId { get; set; }
        public string? CourseCode { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
