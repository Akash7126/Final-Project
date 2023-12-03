namespace Online_Exam_System.ViewModel
{
    public class StudentGivenAnswersViewModel
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int? StudentId { get; set; }
        public bool? IsSelect { get; set; }
        public string? QuestionDescription { get; set; }
        public string? AnswerText { get; set; }
        public int? QuestionTypeId { get; set; }
        public int AnswerCount { get; set; }


       
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public List<StudentGivenAnswer> StudentGivenAnswers { get; set; }
        public List<DistinctQuestionViewModel> DistinctQuestions { get; set; }
        public int TotalMarks { get; set; }
        public DateTime Time { get; set; }
    }

    public class DistinctQuestionViewModel
    {
        public int ExamId { get; set; }
        public string ExamTitle { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CourseTittle { get; set; }
        public string CourseCode { get; set; }
        public string DepartmentName { get; set; }
        public string? QuestionDescription { get; set; }
        public string? AnswerText { get; set; }
        public int? QuestionTypeId { get; set; }
    }
}
