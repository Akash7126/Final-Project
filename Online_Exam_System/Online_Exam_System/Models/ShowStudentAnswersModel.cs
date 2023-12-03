namespace Online_Exam_System.Models
{
    public class ShowStudentAnswersModel
    {
        public List<Question> Questions { get; set; }
        public Dictionary<int, List<int>> SelectedAnswers { get; set; }
    }
}
