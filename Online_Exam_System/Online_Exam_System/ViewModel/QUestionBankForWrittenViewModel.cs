namespace Online_Exam_System.ViewModel
{
    public class QUestionBankForWrittenViewModel
    {

        public int WrittenQuestionId { get; set; }
        public string QuestionDescription { get; set; }
        public double Mark { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CourseTitle { get; set; }
        public int ExamId { get; set; }
        public string? ExamTitle { get; set; }
      
        public int CourseId { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseTittle { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int TeacherId { get; set; }
        public string? Description { get; set; }

        public int StudentId { get; set; }
    }
}
