namespace Online_Exam_System.ViewModel
{
    public class QuestionAnsweViewModel
    {
        public int CourseId { get; set; }
        public int QuestionId { get; set; }
        public string? QuestionDescription { get; set; }
        public string? editordata1 { get; set; }
        public string? editordata2 { get; set; }
        public string? editordata3 { get; set; }
        public string? editordata7 { get; set; }
        public string editordata1Answer { get; set; }
        public string editordata7Answer { get; set; }
        public string editordata2Answer { get; set; }
        public string editordata3Answer { get; set; }
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
