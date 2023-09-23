﻿using System;
using System.Collections.Generic;

namespace Online_Exam_System.Models
{
    public partial class Answer
    {
        public int AnswerId { get; set; }
        public int? QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public bool? IsCorrect { get; set; }

        public virtual Question? Question { get; set; }
    }
}
