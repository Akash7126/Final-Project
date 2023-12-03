namespace Online_Exam_System.ViewModel
{
    public class QuizResultViewModel
    {

        public int ExamId { get; set; }
        public int TotalMarks { get; set; }
        public int NumberOfAnsweredQuestions { get; set; }
        public int OverallTotalMarks { get; set; }
        public int OverallTotalQuestions { get; set; }
        public double Percentage { get; set; }
    }
}
