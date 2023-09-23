namespace Online_Exam_System.ViewModel
{
    public class QuestionAnsweViewModel
    {
        public int CourseId { get; set; }
        public int QuestionId { get; set; }
        public string? QuestionDescription { get; set; }
        public string? Question1 { get; set; }
        public string? Question2 { get; set; }
        public string? Question3 { get; set; }
        public string? Question7 { get; set; }
        public string Question1Answer { get; set; }
        public string Question7Answer { get; set; }
        public string Question2Answer { get; set; }
        public string Question3Answer { get; set; }
        public int? QuestionTypeId { get; set; }
        public decimal? Mark { get; set; }
        public int? QuestionOrder { get; set; }
        public int AnswerId { get; set; }
        public string? AnswerText { get; set; }
        public bool? IsCorrect { get; set; }

        public virtual Question? Question { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
