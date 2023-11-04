namespace Online_Exam_System.ViewModel
{
    public class QuestionAnswerForStudentViewModel
    {
        public int QuestionId { get; set; }
        public string? QuestionDescription { get; set; }
        public int? QuestionTypeId { get; set; }
        public decimal? Mark { get; set; }
        public int? QuestionOrder { get; set; }
        public int? CourseId { get; set; }
        public int? TeacherId { get; set; }
        public int? ExamId { get; set; }
        public int AnswerId { get; set; }
        public string? AnswerText { get; set; }
        public bool? IsCorrect { get; set; }
       
    }
}
